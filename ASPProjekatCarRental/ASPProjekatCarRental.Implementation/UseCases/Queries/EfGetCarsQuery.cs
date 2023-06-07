using ASPProjekatCarRental.Application.UseCases;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
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
    public class EfGetCarsQuery : EfUseCase, IGetCarsQuery
    {
        public EfGetCarsQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "Get Cars Query";

        public string Description => "Use Case for getting cars information";

        public PagedResponse<ResponseSearchCarDto> Execute(BaseSearchWithIsRented request)
        {
            var cars = Context.Cars.Include(x => x.Model).ThenInclude(x => x.Manufacturer)
                                   .Include(x => x.SpecificationCar).ThenInclude(x => x.SSpecificationSpecificationValue).ThenInclude(x => x.Specification)
                                   .Include(x => x.SpecificationCar).ThenInclude(x => x.SSpecificationSpecificationValue).ThenInclude(x => x.SpecificationValue)
                                   .Include(x => x.Registrations).ThenInclude(x => x.RegistrationPlate)
                                   .Include(x=> x.Rentings)
                                   .AsQueryable();

            if (request.Keyword != null)
            {
                cars = cars.Where(x => (x.Model.Manufacturer.Manufacturer_Name.Contains(request.Keyword)) ||
                                       (x.Model.ModelName.Contains(request.Keyword)) ||
                                       (x.Registrations.Where(y => y.RegistrationPlate.Plate.Contains(request.Keyword)).Any()));
            }


            if (request.StartOfRent != null && request.EndOfRent != null)
            {
                cars = cars.Where(car => car.Rentings.All(renting => renting.EndtDate < request.StartOfRent || renting.StartDate > request.EndOfRent && renting.DeletedAt == null));
            }

            if(request.IsDeleted == false)
            {
                cars = cars.Where(car => car.DeletedAt == null);
            }

            if(request.IsDeleted == true)
            {
                cars = cars.Where(car => car.DeletedAt != null);
            }


            if (request.PerPage == null || request.PerPage <1)
            {
                request.PerPage = 15;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.PerPage = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var result = new PagedResponse<ResponseSearchCarDto>();

            result.TotalCount = cars.Count();

            result.Data = cars.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ResponseSearchCarDto
            {
                Manufacturer = x.Model.Manufacturer.Manufacturer_Name,
                Model = x.Model.ModelName,
                RegistrationPlate = x.Registrations.Select(x => x.RegistrationPlate.Plate).First(),
                Specifications = x.SpecificationCar.Select(y => new SpecificationCarDto
                {
                    SpecificationName = y.SSpecificationSpecificationValue.Specification.SpecificationName,
                    SpecificationValue = y.SSpecificationSpecificationValue.SpecificationValue.Value
                }),
                CarCategory = x.Category.CategoryName,
                PricePerDay = x.Prices.OrderByDescending(y => y.CreatedAt).Select(x => x.PricePerDay).FirstOrDefault(),
                PricePerMonth = x.Prices.OrderByDescending(y => y.CreatedAt).Select(x => x.PricePerMonth).FirstOrDefault(),
                StartOfRent = ((x.Rentings.OrderByDescending(y => y.EndtDate).Select(y => y.EndtDate).FirstOrDefault() >= DateTime.UtcNow ? (x.Rentings.OrderByDescending(y => y.EndtDate).Select(y => y.StartDate).FirstOrDefault()) : null)),
                EndOfRent = ((x.Rentings.OrderByDescending(y => y.EndtDate).Select(y => y.EndtDate).FirstOrDefault() >= DateTime.UtcNow ? (x.Rentings.OrderByDescending(y=> y.EndtDate).Select(y => y.EndtDate).FirstOrDefault()) : null)),
                EndOfRegistration = x.Registrations.OrderByDescending(x=> x.EndOfRegistration).Select(x=> x.EndOfRegistration).FirstOrDefault()
            }).ToList();
            result.CurrentPage = request.Page.Value;
            result.ItemsPerPage = request.PerPage.Value;

            return result;
        }
    }
}
