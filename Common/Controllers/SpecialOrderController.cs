using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.SpecialOrder.Controllers
{
    public partial class SpecialOrderController : BasePluginController
    {
        public static string ControllerName = nameof(SpecialOrderController).Replace("Controller", "");
        const string Route = "~/Plugins/Widgets.SpecialOrder/Views/SpecialOrder/";

        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;

        public SpecialOrderController(
            IStoreContext storeContext,
            ISettingService settingService,
            ISpecificationAttributeService specificationAttributeService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IWorkContext workContext)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _specificationAttributeService = specificationAttributeService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _workContext = workContext;
        }
    }
}
