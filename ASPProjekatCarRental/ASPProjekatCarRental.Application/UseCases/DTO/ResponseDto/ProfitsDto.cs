using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class ProfitsDto
    {
        public double Yearly { get; set; } 
        public double Monthly { get; set; } 
        public List<decimal> ByMonth { get; set; } = new List<decimal>();
    }
}
