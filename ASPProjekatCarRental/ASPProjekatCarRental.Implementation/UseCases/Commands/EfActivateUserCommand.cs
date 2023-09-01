using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfActivateUserCommand : EfUseCase, IActivateUserCommand
    {
        private IApplicationUser _user;
        public EfActivateUserCommand(CarRentalContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 19;

        public string Name => "Activate User Command";

        public string Description => "Command for activation of certain user";

        public void Execute(ReceiveUserIdDto request)
        {
            var user = Context.Users.Find(request.Id);
            if (user == null)
            {
                throw new EntityNotFoundException("activate user:", request.Id);
            }

            user.DeletedAt = null;
            user.DeletedBy = null;
            user.UpdatedBy = _user.Identity;

            Context.SaveChanges();
        }
    }
}
