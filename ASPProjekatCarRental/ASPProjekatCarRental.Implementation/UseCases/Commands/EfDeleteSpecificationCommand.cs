using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfDeleteSpecificationCommand : EfUseCase, IDeleteSpecificationCommand
    {
        private readonly DeleteSpecificationValidator _validator;
        public EfDeleteSpecificationCommand(CarRentalContext context, DeleteSpecificationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Delete specification command";

        public string Description => "Command for deleting specifications from provided Id";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var specification = Context.Specifications.Find(request);

            Context.Specifications.Remove(specification);
            Context.SaveChanges();

        }
    }
}
