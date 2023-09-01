using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "Get Users Query";

        public string Description => "Query for getting Users from database";

        public PagedResponse<ResponseUserDto> Execute(UserSearchDto request)
        {
            var user = Context.Users.Include(x => x.UserUseCases).ThenInclude(x => x.UseCase).AsQueryable();

            if (request.Keyword != null)
            {
                user = user.Where(x=> x.FirstName.Contains(request.Keyword) ||
                                      x.LastName.Contains(request.Keyword) ||
                                      x.UserName.Contains(request.Keyword));  
            }

            if (request.PerPage == null || request.PerPage < 1)
            {
                request.PerPage = 15;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.PerPage = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var result = new PagedResponse<ResponseUserDto>();

            result.TotalCount = user.Count();

            result.Data = user.Skip(toSkip).Take(request.PerPage.Value).Select(x => new ResponseUserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Address = x.Adress,
                Email = x.Email,
                Phone = x.Email,
                ImagePath = x.ImagePath,
                DeletedAt = x.DeletedAt,
                UserUseCases = x.UserUseCases.Select(y => new UserUseCasesDto
                {
                    UseCaseName = y.UseCase.UseCaseName
                })
            }).ToList();

            result.CurrentPage = request.Page.Value;
            result.ItemsPerPage = request.PerPage.Value;

            return result;
        }
    }
}
