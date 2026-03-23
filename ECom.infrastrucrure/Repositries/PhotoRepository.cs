using ECom.Core.Entites.Product;
using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries
{
    public class PhotoRepository : GenRepository<Photo>, IPhotosRepository
    {
        public PhotoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
