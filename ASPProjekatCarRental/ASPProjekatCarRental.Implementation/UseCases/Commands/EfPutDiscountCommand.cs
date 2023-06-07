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
    public class EfPutDiscountCommand : EfUseCase, IPutDiscountCommand
    {
        private readonly PutDiscountValidator _validator;
        public EfPutDiscountCommand(CarRentalContext context, PutDiscountValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Change the discount command";

        public string Description => "Command for changing discount table";

        public void Execute(ReceiveDiscountDto request)
        {
            _validator.ValidateAndThrow(request);

            var discount = Context.Discounts.Find(request.Id);

            discount.Percentage = request.Percentage;
            discount.StartDate = request.StartDate;
            discount.EndDate = request.EndDate;
            discount.CategoryId = request.CategoryId;

            /*var discountAdd = new Discount
            {
                Percentage = request.Percentage,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CategoryId = request.CategoryId,
            };*/


            Context.SaveChanges();

        }
    }
}
