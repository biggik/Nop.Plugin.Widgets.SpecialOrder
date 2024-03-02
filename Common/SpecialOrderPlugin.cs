using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Plugin.Widgets.SpecialOrder.Components;
using Nop.Plugin.Widgets.SpecialOrder.Controllers;
using Nop.Plugin.Widgets.SpecialOrder.Resources;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using nopLocalizationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.SpecialOrder
{
    public class SpecialOrderPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;

        public bool HideInWidgetList => false;

        public SpecialOrderPlugin(
            IWebHelper webHelper,
            ILocalizationService localizationService,
            ILanguageService languageService,
            IStoreContext storeContext,
            ISettingService settingService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
            _languageService = languageService;
            _storeContext = storeContext;
            _settingService = settingService;
#if DEBUG
            DebugInitialize();
#endif
        }

#if DEBUG
        private static bool _debugInitialized = false;

        private void DebugInitialize()
        {
            if (_debugInitialized)
                return;

            _debugInitialized = true;

            ResourceHelper().CreateLocaleStringsAsync();
        }
#endif

        private LocaleStringHelper<LocaleStringResource> ResourceHelper()
        {
            return new LocaleStringHelper<LocaleStringResource>
            (
                pluginAssembly: GetType().Assembly,
                languageCultures: from lang in _languageService.GetAllLanguagesAsync().Result select (lang.Id, lang.LanguageCulture),
                getResource: (resourceName, languageId) => _localizationService.GetLocaleStringResourceByNameAsync(resourceName, languageId, false),
                createResource: (languageId, resourceName, resourceValue) => new LocaleStringResource { LanguageId = languageId, ResourceName = resourceName, ResourceValue = resourceValue },
                insertResource: (lsr) => _localizationService.InsertLocaleStringResourceAsync(lsr),
                updateResource: (lsr, resourceValue) => { lsr.ResourceValue = resourceValue; return _localizationService.UpdateLocaleStringResourceAsync(lsr); },
                deleteResource: (lsr) => _localizationService.DeleteLocaleStringResourceAsync(lsr),
                areResourcesEqual: (lsr, resourceValue) => lsr.ResourceValue == resourceValue
            );
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public async Task<IList<string>> GetWidgetZonesAsync()
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<SpecialOrderWidgetSettings>(storeScope);

            return string.IsNullOrWhiteSpace(settings.WidgetZones)
                ? new List<string>()
                : settings.WidgetZones.Split(';').ToList();
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl() 
            => $"{_webHelper.GetStoreLocation()}Admin/{SpecialOrderController.ControllerName}/{nameof(SpecialOrderController.Configure)}";

        /// <summary>
        /// Install plugin
        /// </summary>
        public async override Task InstallAsync()
        {
            await _settingService.SaveSettingAsync(new SpecialOrderWidgetSettings
            {
                WidgetZones = PublicWidgetZones.ProductBoxAddinfoAfter
                              + ";"
                              + PublicWidgetZones.ProductPriceTop
            });

            await ResourceHelper().CreateLocaleStringsAsync();

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<SpecialOrderWidgetSettings>();

            await ResourceHelper().DeleteLocaleStringsAsync();

            await base.UninstallAsync();
        }

        public Type GetWidgetViewComponent(string widgetZone)
            => typeof(WidgetsProductSpecialOrderViewComponent);

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var contentMenu = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Content Management");
            if (contentMenu == null)
            {
                // Unable to find the "Configure" menu, create our own menu container
                contentMenu = new SiteMapNode()
                {
                    SystemName = "SpecialOrder",
                    Title = SpecialOrderResources.Configure,
                    Visible = true,
                    IconClass = "fa-cubes"
                };
                rootNode.ChildNodes.Add(contentMenu);
            }

            async Task<string> T(string format) => await _localizationService.GetResourceAsync(format) ?? format;

            foreach (var item in new List<(string caption, string controller, string action)>
            {
                (await T(SpecialOrderResources.Configure), SpecialOrderController.ControllerName, nameof(SpecialOrderController.Configure))
            })
            {
                contentMenu.ChildNodes.Add(new SiteMapNode
                {
                    SystemName = $"{item.controller}.{item.action}",
                    Title = item.caption,
                    ControllerName = item.controller,
                    ActionName = item.action,
                    Visible = true,
                    IconClass = "fa-dot-circle-o",
                    RouteValues = new RouteValueDictionary {
                    { "area", "Admin" }
                },
                });
            }
        }
    }
}