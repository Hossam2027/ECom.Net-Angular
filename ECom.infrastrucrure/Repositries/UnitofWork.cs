using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries
{
    public  class UnitofWork : IUnitofWork
    {
        public IProductRepository productRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }
        public IPhotosRepository photosRepository { get; private set; }
        public AppDbContext DbContext { get; }




        public UnitofWork(AppDbContext dbContext)
        {
            productRepository = new ProductRepository(dbContext);
            categoryRepository = new CategortRepository(dbContext);
            photosRepository = new PhotoRepository(dbContext);
            DbContext = dbContext;
        }

    }
}
