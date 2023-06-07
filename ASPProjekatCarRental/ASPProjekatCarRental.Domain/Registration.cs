using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Registration : Entity
    {
        public DateTime StartOfRegistration { get; set; }
        public DateTime EndOfRegistration { get; set;}

        public int RegistrationPlateId { get; set; }
        public int CarId { get; set; }

        public virtual RegistrationPlate RegistrationPlate { get; set; }
        public virtual Car Car { get; set; }
    }
}
