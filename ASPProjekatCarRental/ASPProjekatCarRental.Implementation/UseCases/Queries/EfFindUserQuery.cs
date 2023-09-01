using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfFindUserQuery : EfUseCase, IFindUserQuery
    {
        public EfFindUserQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "Find User Query";

        public string Description => "Find User Query";

        public ResponseUserDto Execute(int request)
        {
            var user = Context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Id == request);

            if(user == null)
            {
                throw new EntityNotFoundException("User", request);
            }

            var result = new ResponseUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Adress,
                Phone = user.Phone,
                ImagePath = user.ImagePath,
                UserUseCases = user.UserUseCases.Select(x => new UserUseCasesDto
                {
                    UseCaseName = x.UseCase.UseCaseName

                })
            };

            return result;
        }
    }
}
