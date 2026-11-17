using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Core.Entites
{
    public class address: BaseEntity<int>

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
