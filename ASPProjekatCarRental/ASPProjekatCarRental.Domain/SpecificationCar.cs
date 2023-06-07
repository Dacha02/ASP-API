using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class SpecificationCar
    {
        public int SSpecificationValueId { get; set; }
        public int CarId { get; set; }

        public virtual SpecificationSpecificationValue SSpecificationSpecificationValue { get; set; }
        public virtual Car Car { get; set; }

    }
}
