using ECom.Core.DTO;
using ECom.Core.Entites.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries.Service
{
    public interface IOrderServices
    {
        Task<Orders> CreateOrderAsync(OdrerDTO odrerDTO , string buyerId);
        Task<IReadOnlyList<Orders>> GetOrdersForUserAsync(string buyerId);
        Task<Orders> GetOrderByIdAsync(int id, string buyerId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<DeliveryMethod> GetDeliveryMethodByIdAsync(int id, string buyerId);
    }
}
