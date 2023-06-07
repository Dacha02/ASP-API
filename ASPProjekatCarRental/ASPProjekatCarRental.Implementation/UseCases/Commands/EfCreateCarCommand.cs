using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using ASPProjekatCarRental.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfCreateCarCommand : EfUseCase, ICreateCarCommand
    {
        private readonly InsertCarValidator _validator;
        public EfCreateCarCommand(CarRentalContext context, InsertCarValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Insert new car command";

        public string Description => "Command for inserting a new car with existing data in other tables!";

        public void Execute(ReceiveCarDto request)
        {
            _validator.ValidateAndThrow(request);

            var registrationPlate = new RegistrationPlate
            {
                Plate = request.RegistrationPlate
            };

            var registration = new List<Registration>
            {
                new Registration
                {
                RegistrationPlate = registrationPlate,
                StartOfRegistration = request.StartOfRegistration,
                EndOfRegistration = request.EndOfRegistration
                }
            };
            
            var price = new List<Price>
            {
                new Price
                {
                PricePerDay = request.PricePerDay,
                PricePerMonth = request.PricePerMonth
                }
            };
            

            var specSpecCar = new List<SpecificationCar>();

            foreach (var specSpecCarId in request.SpecSpecValues)
            {
                var objSpecSpecCar = new SpecificationCar
                {
                    SSpecificationValueId = specSpecCarId
                };

                specSpecCar.Add(objSpecSpecCar);
            }

            var car = new Car
            {
                ModelId = request.ModelId,
                RentPlaceId = request.RentPlaceId,
                CategoryId = request.CategoryId,
                ImagePath = request.ImagePath,
                Prices = price,
                Registrations = registration,
                SpecificationCar = specSpecCar
            };

            Context.Cars.Add(car);

            Context.SaveChanges();
        }
    }
}
