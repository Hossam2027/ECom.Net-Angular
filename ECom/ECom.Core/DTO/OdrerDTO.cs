using ECom.Core.Entites.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.DTO
{
    public class OdrerDTO
    {
        public int DeliveryMethodId { get; set; }
        public string basketId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
    }
    public class ShippingAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; }

    }
}
