using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.SpecialOrder
{
    public class SpecialOrderWidgetSettings : ISettings
    {
        public string WidgetZones { get; set; }
        public int SpecialOrderFullPriceSpecId { get; set; }
    }
}
