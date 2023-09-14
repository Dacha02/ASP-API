using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class ResponseSearchAuditLogDto 
    {
        public string UserName { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
        public string UseCaseName { get; set; }
        public string TimeOfExecuction { get; set; }
    }
}
