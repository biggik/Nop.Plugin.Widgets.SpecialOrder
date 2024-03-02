using Nop.Plugin.Widgets.SpecialOrder.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.SpecialOrder.Models
{
    public partial record ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
        }

        [NopResourceDisplayName(ConfigurationResources.WidgetZones)]
        public IList<int> WidgetZones { get; set; }

        [NopResourceDisplayName(ConfigurationResources.SpecialOrderPullPrice)]
        public int SpecialOrderPullPriceSpecId { get; set; }

        public IList<SelectListItem> AvailableWidgetZones { get; set; }
        public IList<SelectListItem> AvailableSpecialOrderCategories { get; set; }
    }
}