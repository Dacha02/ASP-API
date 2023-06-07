using ASPProjekatCarRental.Application.UseCases.Commands;
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
    public class EfDeleteRentCommand : EfUseCase, IDeleteRentCommand
    {
        private readonly DeleteRentValidator _validator;
        public EfDeleteRentCommand(CarRentalContext context, DeleteRentValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "Cancel rent command";

        public string Description => "This command can execute only admin user";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var rent = Context.Rentings.Find(request);

            rent.DeletedAt = DateTime.UtcNow;

            Context.SaveChanges();
        }
    }
}
