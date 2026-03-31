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
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IPhotosRepository PhotosRepository { get; private set; }
        public AppDbContext DbContext { get; }




        public UnitofWork(AppDbContext dbContext)
        {
            ProductRepository = new ProductRepository(dbContext);
            CategoryRepository = new CategortRepository(dbContext);
            PhotosRepository = new PhotoRepository(dbContext);
            DbContext = dbContext;
        }

    }
}
