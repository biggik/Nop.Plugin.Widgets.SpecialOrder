using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.SpecialOrder.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.SpecialOrder.Services
{
    public partial class SpecialOrderService : ISpecialOrderService
    {
        #region Constants
        private const string _prefix = "Nop.status.specialOrder.";
        private readonly static string _productKey = _prefix + "specialOrderproduct-{0}";

        private readonly CacheKey ProductKey = new(_productKey, _prefix);
        #endregion

        #region Fields
        private static int _specialPriceSpecId = -1;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IPriceFormatter _priceFormatter;
        private readonly ISettingService _settingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private ILookup<int, int> _specialPrice;
        #endregion

        #region Ctor
        public SpecialOrderService(
            IStaticCacheManager cacheManager,
            ISpecificationAttributeService specificationAttributeService,
            IStoreContext storeContext,
            IPriceFormatter priceFormatter,
            ISettingService settingService)
        {
            _cacheManager = cacheManager;
            _specificationAttributeService = specificationAttributeService;
            _storeContext = storeContext;
            _priceFormatter = priceFormatter;
            _settingService = settingService;
        }
        #endregion

        private async Task<int> GetIdAsync()
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<SpecialOrderWidgetSettings>(storeScope);

            if (settings.SpecialOrderFullPriceSpecId > 0)
            {
                _specialPrice = (await _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttributeAsync(settings.SpecialOrderFullPriceSpecId))
                            .ToLookup(x => x.Id, y => y.SpecificationAttributeId);
            }
            return settings.SpecialOrderFullPriceSpecId;
        }

        private IStaticCacheManager CacheImpl => _cacheManager;

        private CacheKey CreateKey(CacheKey cacheKey, params object[] arguments)
        {
            return CacheImpl.PrepareKeyForShortTermCache(cacheKey, arguments);
        }

        public async virtual Task<SpecialOrderModel> GetByProductIdAsync(int productId)
        {
            var key = CreateKey(ProductKey, productId);
            return await _cacheManager.GetAsync(key, async () =>
            {
                if (_specialPriceSpecId == -1)
                {
                    _specialPriceSpecId = await GetIdAsync();
                }

                if (_specialPrice == null)
                {
                    return null;
                }
                
                var productSpecificationAttributes = await _specificationAttributeService
                        .GetProductSpecificationAttributesAsync(productId);
                if (productSpecificationAttributes == null || productSpecificationAttributes.Count == 0)
                {
                    return null;
                }

                var number = productSpecificationAttributes
                        .FirstOrDefault(x => _specialPrice.Contains(x.SpecificationAttributeOptionId));
                if (number == null)
                {
                    return null;
                }

                if (!decimal.TryParse(number.CustomValue, out var price))
                {
                    return null;
                }

                return new SpecialOrderModel
                {
                    ProductId = productId,
                    FullPriceValue = price,
                    FullPrice = await _priceFormatter.FormatPriceAsync(price)
                };
            });
        }
    }
}
