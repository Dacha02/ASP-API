using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Price : Entity
    {
        public decimal PricePerDay { get; set; }
        public decimal PricePerMonth { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
