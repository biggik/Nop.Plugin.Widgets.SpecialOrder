using Nop.Plugin.Widgets.SpecialOrder.Models;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.SpecialOrder.Services
{
    public partial interface ISpecialOrderService
    {
        Task<SpecialOrderModel> GetByProductIdAsync(int productId);
    }
}
