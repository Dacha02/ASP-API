using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfChangeIsPaidCommand : EfUseCase, IChangeIsPaidCommand
    {
        public EfChangeIsPaidCommand(CarRentalContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Change Is Paid Command";

        public string Description => "Change Is Paid Command";

        public void Execute(ReceiveRentIdDto request)
        {
            var rent = Context.Rentings.Find(request.Id);

            if(rent == null) 
            {
                throw new EntityNotFoundException(Name, Id);
            }

            rent.IsPaid = true;

            Context.SaveChanges();
        }
    }
}
