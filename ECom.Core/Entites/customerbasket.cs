using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Entites
{
    public  class customerbasket
    {
        public customerbasket()
        {
            
        }
        public customerbasket(string Id)
        {
            id = Id;
        }
        public string id { get; set; }
        public List<basketitem> Items { get; set; } = new List<basketitem>();
    }
}
