using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.Person.CardAddress;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class InsertRentValidator : AbstractValidator<ReceiveRentingDto>
    {
        public InsertRentValidator(CarRentalContext context)
        {
            /*
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("UserId is required!")
                .Must(x => context.Users.Any(u => u.Id == x)).WithMessage("User with id: {PropertyValue} doesn't exist");*/

            RuleFor(x => x.CarId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("CarId is required!")
                .Must(x => context.Cars.Any(c => c.Id == x)).WithMessage("Car with id: {PropertyValue} doesn't exist!")
                .Must(x => context.Cars.Where(c => c.Id == x).Select(y => y.DeletedAt).FirstOrDefault() == null).WithMessage("That car is no longer available");

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThanOrEqualTo(x => DateTime.UtcNow).WithMessage("Start of rent needs to be in the future");
                

            RuleFor(x => x.EndDate)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("End date is required")
                   .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("{PropertyName} must be greather or equal to {ComparisonProperty}");

            RuleFor(x=> x)
                   .Must(request=> BeCarAvailable(request, context))
                   .WithMessage("Car is not available in that period.");

        }
        private bool BeCarAvailable(ReceiveRentingDto request, CarRentalContext context)
        {
            var overlappingRentings = context.Rentings
                .Where(r => r.CarId == request.CarId &&
                            request.StartDate <= r.EndtDate &&
                            request.EndDate >= r.StartDate && r.DeletedAt == null)
                .ToList();

            return overlappingRentings.Count == 0;
        }
    }
}
