using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.SpecialOrder.Models;
using Nop.Plugin.Widgets.SpecialOrder.Services;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Checkout;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.SpecialOrder.Components
{
    [ViewComponent(Name = "WidgetsProductSpecialOrder")]
    public class WidgetsProductSpecialOrderViewComponent : NopViewComponent
    {
        private readonly ISpecialOrderService _specialOrderService;

        public WidgetsProductSpecialOrderViewComponent(ISpecialOrderService specialOrderService)
        {
            _specialOrderService = specialOrderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(RouteValueDictionary values)
        {
            int? productId = null;
            object data = values["additionalData"];
            if (data != null)
            {
                string view = null;
                if (data is ProductDetailsModel pdm)
                {
                    productId = pdm.Id;
                    view = "~/Plugins/Widgets.SpecialOrder/Views/Shared/Components/WidgetProductSpecialOrder/Default.cshtml";
                }
                else if (data is ProductOverviewModel pom)
                {
                    productId = pom.Id;
                    view = "~/Plugins/Widgets.SpecialOrder/Views/Shared/Components/WidgetProductSpecialOrder/CatalogList.cshtml";
                }

                if (productId.HasValue && view != null)
                {
                    var model = await _specialOrderService.GetByProductIdAsync(productId.Value);
                    if (model != null)
                    {
                        return View(view, model);
                    }
                }
            }

            return Content("");
        }
    }
}
