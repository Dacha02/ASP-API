using ASPProjekatCarRental.Application.UseCases.Commands;
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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        private readonly DeleteUserValidator _validator;
        private IApplicationUser _user;
        public EfDeleteUserCommand(CarRentalContext context, DeleteUserValidator validator, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 15;

        public string Name => "Command for deleting users";

        public string Description => "Only admin is allowed to delete user";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var user = Context.Users.Find(request);

            user.DeletedAt = DateTime.Now;
            user.DeletedBy = _user.Identity;

            Context.SaveChanges();
        }
    }
}
