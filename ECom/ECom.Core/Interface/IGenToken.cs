using ECom.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Interface
{
    public  interface IGenToken
    {
       string GetandCreateToken(AppUser appUser);
    }
}
