using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class SpecificationSpecificationValue : Entity
    {
        public int SpecificationId { get; set; }
        public int SpecificationValueId { get; set; }

        public virtual Specification Specification { get; set; }
        public virtual SpecificationValue SpecificationValue { get; set; }

        public virtual ICollection<SpecificationCar> SpecificationCars { get; set; } = new List<SpecificationCar>();
    }
}
