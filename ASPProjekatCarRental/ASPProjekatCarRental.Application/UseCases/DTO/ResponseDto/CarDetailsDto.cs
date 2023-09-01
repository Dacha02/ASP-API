using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class CarDetailsDto
    {
        public IEnumerable<ModelManufacturerInfoDto> Models { get; set; } = new List<ModelManufacturerInfoDto>();
        public IEnumerable<RentPlaceInfoDto> RentPlaces { get; set; } = new List<RentPlaceInfoDto>();
        public IEnumerable<CategoryInfoDto> Categories { get; set; } = new List<CategoryInfoDto>();
        public IEnumerable<SpecSpecValuesDto> Specifications { get; set; } = new List<SpecSpecValuesDto>();
    }

    public class SpecSpecValuesDto
    {
        public int Id { get; set; }
        public string SpecificationName { get; set; }
        public int SpecificationId { get; set; }
        public string SpecificationValues { get; set; }
        public int SpecificationValueId { get; set; }
    }

    public class Specifications
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SpecValues
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ModelManufacturerInfoDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public ManufacturerInfo Manufacturers { get; set; }
    }

    public class ManufacturerInfo
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }

    public class RentPlaceInfoDto
    {
        public int RentPlaceId { get; set; }
        public string RentAddress { get; set; }
    }

    public class CategoryInfoDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
