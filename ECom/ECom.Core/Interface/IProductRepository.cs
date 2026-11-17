using ECom.Core.DTO;
using ECom.Core.Entites.Product;
using ECom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
       public  Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams);
    }
}
