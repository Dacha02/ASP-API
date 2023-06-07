using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto
{
    public class ReceivePriceDto
    {
        public decimal PricePerDay { get; set; }
        public decimal PricePerMonth { get; set; }
        public int CarId { get; set; }

    }
}
