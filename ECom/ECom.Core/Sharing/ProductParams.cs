using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Sharing
{
    public  class ProductParams
    {
        public string? sort { get; set; }
        public int? categoryid { get; set; }    
        public int MaxPagesize { get; set; } = 8;

        private int _pagesize = 8;

        public int pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value >MaxPagesize?MaxPagesize:value; }
        }
        public int pageNumber { get; set; } = 1;
        public string? Search { get; set; }

    }
}
