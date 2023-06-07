using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Domain
{
    public class AuditLog : Entity
    {
        public string Data { get; set; } 
        public bool IsAuthorized { get; set; }
        public string UseCaseName { get; set; }
        public DateTime TimeOfExecution { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
