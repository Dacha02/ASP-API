using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class Renting : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public decimal SumCost { get; set; }
        public string RentAdress { get; set; }
        public bool IsPaid { get; set; }

        public int UserId { get; set; }
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
    }
}
