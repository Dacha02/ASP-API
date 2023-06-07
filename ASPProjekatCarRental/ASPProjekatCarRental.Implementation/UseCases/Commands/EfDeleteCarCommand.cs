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
    public class EfDeleteCarCommand : EfUseCase, IDeleteCarCommand
    {
        private IApplicationUser _user;
        private readonly CheckCarIdValidator _validator;
        public EfDeleteCarCommand(CarRentalContext context, IApplicationUser user, CheckCarIdValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Set DeletedAt and DeletedBy";

        public string Description => "Set two propertyes to have values";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);

            var car = Context.Cars.Find(request);

            car.DeletedAt = DateTime.UtcNow;
            car.DeletedBy = _user.Identity;

            Context.SaveChanges();
        }
    }
}
