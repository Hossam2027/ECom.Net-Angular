using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites.Product;
using ECom.Core.Interface;
using ECom.Core.Sharing;
using ECom.infrastrucrure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries
{
    public class ProductRepository : GenRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext , IMapper mapper) : base(appDbContext)
        {
            AppDbContext = appDbContext;
            Mapper = mapper;
        }

     
        public AppDbContext AppDbContext { get; }
        public IMapper Mapper { get; }

        public  async Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams)
        {
            var query = AppDbContext.Products.Include(x => x.Photos).Include(x => x.Category)
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(productParams.Search)) //Search by name or description
            {
                query = query.Where(x => x.Name.ToLower().Contains(productParams.Search)
                || x.Description.ToLower().Contains(productParams.Search));
            }

            if (productParams.categoryid.HasValue) //Filter by categoryid

            { query = query.Where(x => x.CategoryId == productParams.categoryid); }
            if (!string.IsNullOrEmpty(productParams.sort))
            {
                query = productParams.sort switch
                {
                    "PriceAsc" => query.OrderBy(x => x.NewPrice),
                    "PriceDes" => query.OrderByDescending(x => x.NewPrice),
                    _ => query.OrderBy(x => x.Name),
                };
            }
          
            query = query.Skip((productParams.pageNumber - 1) * productParams.pagesize).Take(productParams.pagesize); //Pagination

            var Result = Mapper.Map<List<ProductDTO>>(query);
            return Result;
        }

       
    }
}
