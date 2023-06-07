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
    public class PutDiscountValidator : AbstractValidator<ReceiveDiscountDto>
    {
        public PutDiscountValidator(CarRentalContext context)
        {
            RuleFor(x => x.Id)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Discount id is required!")
                   .Must(x => context.Discounts.Any(y => y.Id == x)).WithMessage("Discount with id: {ProperyValue} is not foud.");

            RuleFor(x => x.Percentage)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Percentage is required!")
                   .Must(x => decimal.Floor(x) == x).WithMessage("Percenatage needs to be a whole number!");

            RuleFor(x => x.StartDate)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Start date is required!")
                   .Must(x=> x>= DateTime.UtcNow).WithMessage("Can't set the start date to be in the past")
                   .Must((discount, startDate) => BeGreaterThanStartDate(startDate, discount.Id, context))
                   .WithMessage("You can't set start date to be older than previous start date of discount");

            RuleFor(x => x.EndDate)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("End date is required!")
                   .Must((startDate, EndDate) => startDate.StartDate <= EndDate).WithMessage("End date of discount can't be older than start date!")
                   .Must((discount, endDate) => BeGreaterThanEndtDate(endDate, discount.Id, context))
                   .WithMessage("End date can't be older than previous end date of discount");

            RuleFor(x => x.CategoryId)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Category id is required!")
                   .Must(x => context.Categories.Any(y => y.Id == x)).WithMessage("Category with id: {PropertyValue} doesn't exist");
        }

        private bool BeGreaterThanStartDate(DateTime startDate, int discountId, CarRentalContext context)
        {
            var previouseStartDate = context.Discounts
                .Where(r => r.Id == discountId)
                .Select(r => r.StartDate)
                .FirstOrDefault();

            return startDate > previouseStartDate;
        }

        private bool BeGreaterThanEndtDate(DateTime endDate, int discountId, CarRentalContext context)
        {
            var previouseEndDate = context.Discounts
                .Where(r => r.Id == discountId)
                .Select(r => r.EndDate)
                .FirstOrDefault();

            return endDate > previouseEndDate;
        }
    }
}
