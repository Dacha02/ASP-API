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
    public class DeleteSpecificationValidator : AbstractValidator<int>
    {
        public DeleteSpecificationValidator(CarRentalContext context)
        {
            RuleFor(x => x)
                   .Must(x => context.Specifications.Any(y => y.Id == x)).WithMessage("Specification with Id : {PropertyValue} doesn't exist")
                   .Must(x => !context.SpecificationSpecificationValues.Any(y => y.SpecificationId == x)).WithMessage("That specification is in use and you can't delete it!");
        }
    }
}
