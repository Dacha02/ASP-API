using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto
{
    public class ReceiveCarDto
    {
        public int ModelId { get; set; }
        public int RentPlaceId { get; set; }
        public int CategoryId { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerMonth { get; set; }
        public string RegistrationPlate { get; set; }
        public string? ImagePath { get; set; }
        public DateTime StartOfRegistration { get; set; }
        public DateTime EndOfRegistration { get; set; }
        public List<int> SpecSpecValues { get; set; }

    }

    public class ReceiveCarDtoWithCarId : ReceiveCarDto
    {
        public int Id { get; set; }
    }
}
