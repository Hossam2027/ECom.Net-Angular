using ECom.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public interface IcustomerBasketRepository
    {
        Task<customerbasket> GetBasketAsync(string id);
        Task<customerbasket> updatebasketAsync(customerbasket basket);
        Task <bool> deletebasketAsync(string id);
    }
}
