using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfGetCarDetailsQuery : EfUseCase, IGetCarDetailsQuery
    {
        public EfGetCarDetailsQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 18;

        public string Name => "Get car details Query";

        public string Description => "Query for returning car details for inserting a car";

        public CarDetailsDto Execute(DummyClass request)
        {
            var models = Context.Models.Include(x => x.Manufacturer).AsQueryable();
            var categories = Context.Categories.AsQueryable();
            var rentingPlaces = Context.RentingPlaces.AsQueryable();
            var specSpecValues = Context.SpecificationSpecificationValues.Include(x=> x.SpecificationValue)
                                                                          .Include(x=> x.Specification)
                                                                          .AsQueryable();

            var carDetailsDto = new CarDetailsDto
            {
                Models = models.Select(model => new ModelManufacturerInfoDto
                {
                    ModelId = model.Id,
                    ModelName = model.ModelName,
                    Manufacturers = new ManufacturerInfo
                    {
                        ManufacturerId = model.Manufacturer.Id,
                        ManufacturerName = model.Manufacturer.Manufacturer_Name
                    }
                }),

                Categories = categories.Select(category => new CategoryInfoDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName
                }),

                RentPlaces = rentingPlaces.Select(rentPlace => new RentPlaceInfoDto
                {
                    RentPlaceId = rentPlace.Id,
                    RentAddress = rentPlace.Adress
                }),

                Specifications = specSpecValues.Select(specSpecValue => new SpecSpecValuesDto
                {
                    Id = specSpecValue.Id,
                    SpecificationName = specSpecValue.Specification.SpecificationName,
                    SpecificationId = specSpecValue.Specification.Id,
                    SpecificationValueId = specSpecValue.SpecificationValue.Id,
                    SpecificationValues = specSpecValue.SpecificationValue.Value
                })
            };

            return carDetailsDto;
        }
    }
}
