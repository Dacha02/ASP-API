using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfFindCarQuery : EfUseCase, IFindCarQuery
    {
        private readonly CheckCarIdValidator _validator;
        public EfFindCarQuery(CarRentalContext context, CheckCarIdValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 11;

        public string Name => "Query for finding specific car";

        public string Description => "Query for finding specific car";

        public ResponseSearchCarDto Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var car = Context.Cars.Include(x => x.Model).ThenInclude(x => x.Manufacturer)
                                  .Include(x=>x.Model)
                                  .Include(x => x.SpecificationCar).ThenInclude(x => x.SSpecificationSpecificationValue).ThenInclude(x => x.Specification)
                                  .Include(x => x.SpecificationCar).ThenInclude(x => x.SSpecificationSpecificationValue).ThenInclude(x => x.SpecificationValue)
                                  .Include(x => x.Registrations).ThenInclude(x => x.RegistrationPlate)
                                  .Include(x => x.Rentings)
                                  .FirstOrDefault(x => x.Id == request);

            if (car != null)
            {
                var result = new ResponseSearchCarDto
                {
                    Manufacturer = car.Model.Manufacturer?.Manufacturer_Name,
                    Model = car.Model?.ModelName,
                    RegistrationPlate = car.Registrations?.Select(x => x.RegistrationPlate.Plate).FirstOrDefault(),
                    Specifications = car.SpecificationCar?.Select(y => new SpecificationCarDto
                    {
                        SpecificationName = y.SSpecificationSpecificationValue?.Specification?.SpecificationName,
                        SpecificationValue = y.SSpecificationSpecificationValue?.SpecificationValue?.Value
                    }),
                    CarCategory = car.Category?.CategoryName,
                    PricePerDay = car.Prices?.OrderByDescending(y => y.CreatedAt).Select(x => x.PricePerDay).FirstOrDefault(),
                    PricePerMonth = car.Prices?.OrderByDescending(y => y.CreatedAt).Select(x => x.PricePerMonth).FirstOrDefault(),
                };

                return result;
            }
            else
            {
                throw new EntityNotFoundException("Greska", request);
            }
        }
    }
}
