using ECom.Core.DTO;
using ECom.Core.Entites;
using ECom.Core.Entites.Order;
using ECom.Core.Entites.Product;
using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECom.Core.Entites.Order;

namespace ECom.infrastrucrure.Repositries.Service
{
    public  class OrderServices : IOrderServices
    {
        public IUnitofWork UnitofWork { get; }
        public AppDbContext Dbcontext { get; }

        public OrderServices(IUnitofWork unitofWork , AppDbContext dbcontext )
        {
            UnitofWork = unitofWork;
            Dbcontext = dbcontext;
        }



        public async Task<Orders> CreateOrderAsync(OdrerDTO odrerDTO, string buyerId)
        {
            var basket = await UnitofWork.CustomerBasketRepository.GetBasketAsync(odrerDTO.basketId);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await UnitofWork.ProductRepository.GetByIdAsync(item.ID);
                var orderItem = new OrderItem
                    (productItem.Id,item.imageUrl , productItem.Name, item.Price, item.Quantity);

                orderItems.Add(orderItem);
            }
            var deliveryMethod = await Dbcontext.DeliveryMethods.FirstOrDefaultAsync(m=>m.Id == odrerDTO.DeliveryMethodId);
            var subtotal = orderItems.Sum(i => i.Price * i.Quantity);
            var order = new Orders
            {
                buyerId = buyerId,
                OrderItems = orderItems,
                ShippingAddress = new ShippingAddress
                {
                    FirstName = odrerDTO.ShippingAddress.FirstName,
                    LastName = odrerDTO.ShippingAddress.LastName,
                    Street = odrerDTO.ShippingAddress.Street,
                    City = odrerDTO.ShippingAddress.City,
                    State = odrerDTO.ShippingAddress.State,
                },
                DeliveryMethod = deliveryMethod,
                SubTotal = subtotal
            };
            await Dbcontext.Orders.AddAsync(order);
            await Dbcontext.SaveChangesAsync();
            return order;
        }

        public Task<DeliveryMethod> GetDeliveryMethodByIdAsync(int id, string buyerId)
        {
            return Dbcontext.DeliveryMethods.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await Dbcontext.DeliveryMethods.AsNoTracking().ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int id, string buyerId)
        {
            var order = await Dbcontext.Orders.Where(o => o.Id == id && o.buyerId == buyerId).
                Include(o => o.OrderItems).Include(o => o.DeliveryMethod).FirstOrDefaultAsync();
            return order;
        }

        public async Task<IReadOnlyList<Orders>> GetOrdersForUserAsync(string buyerId)
        {
            var orders = await Dbcontext.Orders.Where(m=>m.buyerId == buyerId).
                Include(o => o.OrderItems).Include(o => o.DeliveryMethod).ToListAsync();
            return orders;
        }
    }
}
