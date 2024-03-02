using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.SpecialOrder.Models
{
    public partial record SpecialOrderModel : BaseNopEntityModel
    {
        public SpecialOrderModel()
        {
        }

        public int ProductId { get; set; }

        public string FullPrice { get; set; }

        /// <summary>
        /// The full price of the product. The product's price should be the down payment of the product
        /// </summary>
        public decimal FullPriceValue { get; set; }
    }
}