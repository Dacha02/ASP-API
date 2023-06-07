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
    public class EfInsertPriceCommand : EfUseCase, ICreatePriceDto
    {
        private readonly InsertPricesValidator _validator;
        public EfInsertPriceCommand(CarRentalContext context, InsertPricesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Command for inserting prices";

        public string Description => "With this command you can insert prices for certain cars.";

        public void Execute(ReceivePriceDto request)
        {
             _validator.ValidateAndThrow(request);

            var prices = new Price
            {
                PricePerDay = request.PricePerDay,
                PricePerMonth = request.PricePerMonth,
                CarId = request.CarId
            };

            Context.Prices.Add(prices);
            Context.SaveChanges();
        }
    }
}
