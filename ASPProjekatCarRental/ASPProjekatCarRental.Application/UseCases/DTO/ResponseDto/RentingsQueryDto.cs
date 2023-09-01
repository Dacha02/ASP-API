using ASPProjekatCarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class RentingsQueryDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        public decimal SumCost { get; set; }
        public string RentAddress { get; set; }
    }

    public class RentingsQueryExtended : RentingsQueryDto
    {
        public bool IsPaid { get; set; }
        public string Username { get; set; }
    }

}
