using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public  interface IUnitofWork 
    {
        public ICategoryRepository  categoryRepository { get; }
        public IProductRepository productRepository { get; }
        public IPhotosRepository photosRepository { get; }
    }
}
