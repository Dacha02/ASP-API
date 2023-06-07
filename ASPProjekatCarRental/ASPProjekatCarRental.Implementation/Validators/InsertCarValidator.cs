using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class InsertCarValidator : AbstractValidator<ReceiveCarDto>
    {
        public InsertCarValidator(CarRentalContext context)
        {
            RuleFor(x => x.ModelId)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Model Id is required!")
                  .Must(x => context.Models.Any(y => y.Id == x)).WithMessage("Model doesn't exist");

            RuleFor(x => x.RentPlaceId)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Rent place Id is required!")
                  .Must(x => context.RentingPlaces.Any(y => y.Id == x)).WithMessage("Renting place doesn't exist");

            RuleFor(x => x.CategoryId)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Category Id is required!")
                  .Must(x => context.Categories.Any(y => y.Id == x)).WithMessage("Category doesn't exist");

            RuleFor(x => x.PricePerDay)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Price per day is required!")
                  .Must(x => x > 15).WithMessage("Price per day must be minimum 15e");

            RuleFor(x => x.PricePerMonth)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Price per month is required!")
                  .Must(x => x > 150).WithMessage("Price per month must be minimum 150e");

            RuleFor(x => x.RegistrationPlate)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Registration plate is required!")
                  .Must(x => !context.RegistrationPlates.Any(y => y.Plate == x)).WithMessage("Registration plate is already in use");

            RuleFor(x => x.StartOfRegistration)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Start of registration is required!")
                  .Must(x=> x> DateTime.UtcNow.AddYears(-1) && x < DateTime.UtcNow.AddYears(1)).WithMessage("Start of registration needs to be in this year");

            RuleFor(x => x.EndOfRegistration)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage("End of registration is required!")
                 .Must((dto, endDate) =>
                 {
                     // Calculate the expected end date based on the start date
                     var expectedEndDate = dto.StartOfRegistration.AddYears(1);

                     // Validate that the end date is exactly one year after the start date
                     return endDate == expectedEndDate;
                 }).WithMessage("End of registration needs to be grather than a start of registartion and need's to be exactly 1 year after start of registration");

            RuleFor(x=> x.SpecSpecValues)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("List of specifications is required")
                   .Must(x=> BeValidElements(x, context))
                   .WithMessage("Invalid elements found in the list.");
        }

        private bool BeValidElements(List<int> elements, CarRentalContext context)
        {
            var existingIds = context.SpecificationSpecificationValues.Select(e => e.Id).ToList();
            return elements.All(e => existingIds.Contains(e));
        }
    }
}
