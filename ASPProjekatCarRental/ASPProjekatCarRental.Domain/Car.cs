using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Car : Entity
    {
        public string ImagePath { get; set; }

        public int ModelId { get; set; }
        public int RentPlaceId { get; set; }
        public int CategoryId { get; set; }

        public virtual RentingPlace RentingPlace { get; set; }
        public virtual Category Category { get; set; }
        public virtual Model Model { get; set; }

        public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public virtual ICollection<SpecificationCar> SpecificationCar { get; set; } = new List<SpecificationCar>();

    }
}
