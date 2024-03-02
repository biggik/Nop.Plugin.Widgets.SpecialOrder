using nopLocalizationHelper;

namespace Nop.Plugin.Widgets.SpecialOrder.Resources
{
    internal static class Cultures
    {
        public const string EN = "en-US";
        public const string IS = "is-IS";
    }

    [LocaleStringProvider]
    public static class SpecialOrderResources
    {
        [LocaleString(Cultures.EN, "Special order !")]
        [LocaleString(Cultures.IS, "Sérpöntun !")]
        public const string Title = "Status.SpecialOrderWidget.SpecialOrder.Title";

        [LocaleString(Cultures.EN, "This is a special order product. The displayed price is a down payment on the product, and the full price is {0}. Once we have received your down payment we will order the product and you will pay the remainder upon delivery.")]
        [LocaleString(Cultures.IS, "Þessi vara er aðeins í boði gegn sérpöntun. Verðið sem er sýnt er innáborgun, en fullt verð er {0}. Þegar við höfum móttekið pöntun þína þá pöntum við vöruna frá okkar birgja og þú greiðir fullnaðargreiðslu við afhendingu vörunnar.")]
        public const string Description = "Status.SpecialOrderWidget.SpecialOrder.Description";

        [LocaleString(Cultures.EN, "Full price:")]
        [LocaleString(Cultures.IS, "Fullt verð:")]
        public const string Price = "Status.SpecialOrderWidget.SpecialOrder.Price";

        [LocaleString(Cultures.EN, "Configure")]
        [LocaleString(Cultures.IS, "Stillingar")]
        public const string Configure = "Status.SpecialOrderWidget.SpecialOrder.Confiure";

        [LocaleString(Cultures.EN, "Special order: ")]
        [LocaleString(Cultures.IS, "Sérpöntun: ")]
        public const string CatalogList = "Status.SpecialOrderWidget.SpecialOrder.CatalogList";

    }

    [LocaleStringProvider]
    public static class ConfigurationResources
    {
        [LocaleString(Cultures.EN, "Widget zones", "In which zones should the widget be displayed")]
        [LocaleString(Cultures.IS, "Birta í", "Hvar á síðunni á að birta íhlutinn")]
        public const string WidgetZones = "Status.SpecialOrderWidget.Configuration.WidgetZones";

        [LocaleString(Cultures.EN, "Special order full price", "Technical specification that contains the full price of the item")]
        [LocaleString(Cultures.IS, "Sérpöntun fullt verð", "Tæknileg lýsing sem er notuð til að geyma fullt verð vöru")]
        public const string SpecialOrderPullPrice = "Status.SpecialOrderWidget.Configuration.SpecialOrderPullPrice";
    }
}
