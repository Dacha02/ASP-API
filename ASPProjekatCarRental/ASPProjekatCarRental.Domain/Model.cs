using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Model : Entity
    {
        public string ModelName { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
