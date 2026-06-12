using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Entites.Order
{
    public class Orders : BaseEntity<int>

    {
        public Orders()
        {
            
        }
        public Orders(string buyerId, decimal subTotal , IReadOnlyList<OrderItem> orderItems , ShippingAddress shippingAddress , DeliveryMethod deliveryMethod)
        {
            this.buyerId = buyerId;
            this.SubTotal = subTotal;
            this.OrderItems = orderItems;
            this.ShippingAddress = shippingAddress;
            this.DeliveryMethod = deliveryMethod;
        }
        public string buyerId { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public Status status { get; set; } = Status.Pending;
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }       

    }
}
