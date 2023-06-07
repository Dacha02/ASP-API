using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class InsertPricesValidator : AbstractValidator<ReceivePriceDto>
    {
        public InsertPricesValidator(CarRentalContext context)
        {
            RuleFor(x => x.PricePerDay)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Price per day is required!")
                   .Must(x => x > 15).WithMessage("Minimum price per day for a car needs to be 15e!");

            RuleFor(x => x.PricePerMonth)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Price per month is required!")
                   .Must(x => x > 150).WithMessage("Minimum price per month for a car needs to be 150e!");

            RuleFor(x=> x.CarId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("CarId is required!")
                    .Must(x=> context.Cars.Any(y=> y.Id == x)).WithMessage("Provided CarId {PropertyValue} doesn't exist!");
        }
    }
}
