using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class CheckCarIdValidator : AbstractValidator<int>
    {
        public CheckCarIdValidator(CarRentalContext context)
        {
            RuleFor(x => x)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Car id is required!")
                   .Must(x => context.Cars.Any(y => y.Id == x)).WithMessage("Provided id: {PropertyValue} doesn't exist!");

        }
    }
}
