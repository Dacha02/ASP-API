using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class ResponseSearchCarDto
    {
        public int CarId { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int? ModelId { get; set; }
        public int? CategoryId { get; set; }
        public int? RentingPlaceId { get; set; }
        public string? RegistrationPlate { get; set; }
        public IEnumerable<SpecificationCarDto>? Specifications { get; set; }
        public string? CarCategory { get; set; }
        public decimal? PricePerDay { get; set; }
        public decimal? PricePerMonth { get; set; }
        public DateTime? DeletedAt { get; set; }

        public DateTime? StartOfRent { get; set; }
        public DateTime? EndOfRent { get; set; }
        public DateTime? StartOfRegistration { get; set; }
        public DateTime? EndOfRegistration { get; set; }
        public string ImagePath { get; set; }
        //public DateTime AvailableFrom { get; set; }
    }

    public class SpecificationCarDto
    {
        public int Id { get; set; }
        public string? SpecificationName { get; set; }
        public string? SpecificationValue { get; set; }
    }
}
