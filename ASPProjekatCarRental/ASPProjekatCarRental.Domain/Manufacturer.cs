using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Manufacturer : Entity
    {
        public string Manufacturer_Name { get; set; }

        public virtual ICollection<Model> Models { get; set; } = new List<Model>();
    }
}
