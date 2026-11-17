using ECom.Core.Entites;
using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.infrastrucrure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries.Service
{
    
    public class PaymentServices : IPaymentServices
        
    {
        public IUnitofWork UnitofWork { get; }
        public IConfiguration Configuration { get; }
        public AppDbContext AppContext { get; }

        public PaymentServices(IUnitofWork unitofWork , IConfiguration configuration , AppDbContext appContext)
        {
            UnitofWork = unitofWork;
            Configuration = configuration;
            AppContext = appContext;
        }


        public async Task<customerbasket> createorupdatePaymentAsync(string basketid, int? deliveryMethodId)
        {
            customerbasket basket = await UnitofWork.CustomerBasketRepository.GetBasketAsync(basketid);
            StripeConfiguration.ApiKey = Configuration["StripeSettings:SecretKey"];
            var shippingprice = 0m;
            if (deliveryMethodId.HasValue)
            {
                var deliveryMethod = await AppContext.DeliveryMethods.FindAsync(deliveryMethodId.Value);
                shippingprice = deliveryMethod.Price;
            }
            foreach (var item in basket.Items)
            {
                var productitem = await AppContext.Products.FindAsync(item.ID);
                if (productitem == null) continue;
              
                {
                    item.Price = productitem.NewPrice;
                }
            }

            PaymentIntentService paymentservice = new PaymentIntentService();
            return basket;

        }

    }
}
