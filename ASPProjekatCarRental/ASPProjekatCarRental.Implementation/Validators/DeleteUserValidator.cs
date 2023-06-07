using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class DeleteUserValidator : AbstractValidator<int>
    {
        public DeleteUserValidator(CarRentalContext context)
        {
            RuleFor(x => x)
                   .Must(x => context.Users.Any(y => y.Id == x)).WithMessage("User with id: {PropertyValue} doesn't exist");
        }
    }
}
