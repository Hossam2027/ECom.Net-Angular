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
    public class CategortRepository : GenRepository<Category>, ICategoryRepository
    {
        public CategortRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
