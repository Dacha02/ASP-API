using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Queries
{
    public class EfGetRentingsQuery : EfUseCase, IGetRentingsQuery
    {
        public EfGetRentingsQuery(CarRentalContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Get all renitngs Query";

        public string Description => "Get all renitngs Query";

        public PagedResponse<RentingsQueryExtended> Execute(BaseSearchWithIsRented request)
        {
            var rentings = Context.Rentings.Include(x => x.Car)
                                           .Include(x => x.Car).ThenInclude(y => y.Model)
                                           .Include(x => x.Car).ThenInclude(y => y.Model).ThenInclude(y => y.Manufacturer)
                                           .Include(x => x.User)
                                           .AsQueryable();

            if (request.Keyword != null)
            {
                rentings = rentings.Where(x => (x.User.UserName.Contains(request.Keyword)) ||
                (x.Car.Model.ModelName.Contains(request.Keyword)) || (x.User.FirstName.Contains(request.Keyword)) ||
                (x.User.LastName.Contains(request.Keyword)));
            }


            if (request.StartOfRent != null && request.EndOfRent != null)
            {
                rentings = rentings.Where((renting => renting.EndtDate < request.StartOfRent || renting.StartDate > request.EndOfRent && renting.DeletedAt == null));
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

            var result = new PagedResponse<RentingsQueryExtended>();

            result.TotalCount = rentings.Count();

            result.Data = rentings.Skip(toSkip).Take(request.PerPage.Value).Select(x => new RentingsQueryExtended
            {
                Id = x.Id,
                StartOfRent = x.StartDate,
                EndOfRent = x.EndtDate,
                SumCost = x.SumCost,
                RentAddress = x.RentAdress,
                Model = x.Car.Model.ModelName,
                Manufacturer = x.Car.Model.Manufacturer.Manufacturer_Name,
                IsPaid = x.IsPaid,
                Username = x.User.UserName
            }).ToList();

            result.CurrentPage = request.Page.Value;
            result.ItemsPerPage = request.PerPage.Value;

            return result;
        }
    }
}
