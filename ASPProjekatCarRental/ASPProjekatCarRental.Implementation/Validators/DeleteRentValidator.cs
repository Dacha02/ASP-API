using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class DeleteRentValidator : AbstractValidator<int>
    {
        public DeleteRentValidator(CarRentalContext context)
        {
            RuleFor(x => x)
                   .Must(x => context.Rentings.Any(y => y.Id == x)).WithMessage("Rent with id: {PropertyValue} doesn't exist")
                   .Must(x => context.Rentings.Where(y => y.Id == x).Select(y => y.DeletedAt).First() == null).WithMessage("That rent is already canceled");
        }
    }
}
