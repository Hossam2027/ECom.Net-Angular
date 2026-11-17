using AutoMapper;
using ECom.Core.Entites;
using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.infrastrucrure.Data;
using StackExchange.Redis;
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
        public IcustomerBasketRepository CustomerBasketRepository { get; }

        public AppDbContext DbContext { get; }
        public IMapper Mapper { get; }


               public UnitofWork(AppDbContext dbContext, IMapper mapper,
            IImageManegmentService imageManegmentService, IConnectionMultiplexer redis
           )

           //    public UnitofWork(AppDbContext dbContext, IMapper mapper,
           // IImageManegmentService imageManegmentService
           //)
        {
            ProductRepository = new ProductRepository(dbContext , mapper);
            CategoryRepository = new CategortRepository(dbContext);
            PhotosRepository = new PhotoRepository(dbContext);
            CustomerBasketRepository = new CustomBasketRepository(redis);
            DbContext = dbContext;
            Mapper = mapper;
        }

    }
}
