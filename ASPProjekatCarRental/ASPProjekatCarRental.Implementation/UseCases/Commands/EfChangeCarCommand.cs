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
    public class EfChangeCarCommand : EfUseCase, IChangeCarCommand
    {
        private readonly ChangeCarValidator _validator;
        public EfChangeCarCommand(CarRentalContext context, ChangeCarValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Change Car Command";

        public string Description => "Command for changing car table";

        public void Execute(ReceiveCarDtoWithCarId request)
        {
            _validator.ValidateAndThrow(request);

            // Pronalazenje auta
            var car = Context.Cars.Find(request.Id);

            // Menjanje podataka u tabeli Car
            car.ModelId = request.ModelId;
            car.RentPlaceId = request.RentPlaceId;
            car.CategoryId = request.CategoryId;
            car.ImagePath = request.ImagePath;


            var priceNew = new List<Price>
            {
                new Price
                {
                    PricePerDay = request.PricePerDay,
                    PricePerMonth = request.PricePerMonth
                }
            };

            car.Prices = priceNew;

            /*// Pronalazenje cene za auto
            var price = Context.Prices.FirstOrDefault(x => x.CarId == request.Id);

            // Menjanje cene za auto
            price.PricePerDay = request.PricePerDay;
            price.PricePerMonth = request.PricePerMonth;*/

            // Pronalazenje resgistracije za automobil
            var registration = Context.Registrations.FirstOrDefault(x => x.CarId == request.Id);

            // Menjanje registracije za automobil
            registration.StartOfRegistration = request.StartOfRegistration;
            registration.EndOfRegistration = request.EndOfRegistration;

            // Pronalazenje registarske oznake
            var registrationPlate = Context.RegistrationPlates.FirstOrDefault(x => x.Id == registration.RegistrationPlateId);

            // Menjanje registarske oznake
            registrationPlate.Plate = request.RegistrationPlate;

            var specificationsCar = Context.SpecificationCars.Where(x => x.CarId == request.Id).ToList();

            Context.SpecificationCars.RemoveRange(specificationsCar);

            var specSpecCar = new List<SpecificationCar>();

            foreach (var specSpecCarId in request.SpecSpecValues)
            {
                var objSpecSpecCar = new SpecificationCar
                {
                    SSpecificationValueId = specSpecCarId,
                    Car = car
                };

                specSpecCar.Add(objSpecSpecCar);
            }

            car.SpecificationCar = specSpecCar;

            Context.SaveChanges();

        }
    }
}
