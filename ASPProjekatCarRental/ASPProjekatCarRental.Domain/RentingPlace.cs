using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class RentingPlace : Entity
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
