using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public  interface IUnitofWork 
    {
        public ICategoryRepository  CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotosRepository PhotosRepository { get; }
        public IcustomerBasketRepository CustomerBasketRepository { get; }
    }
}
