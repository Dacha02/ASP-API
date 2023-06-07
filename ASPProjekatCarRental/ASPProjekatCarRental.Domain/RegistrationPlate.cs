using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class RegistrationPlate : Entity
    {
        public string Plate { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
