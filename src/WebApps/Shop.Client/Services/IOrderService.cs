using Shop.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Client.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
