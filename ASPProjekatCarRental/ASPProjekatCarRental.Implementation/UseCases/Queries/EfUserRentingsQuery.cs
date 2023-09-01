using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto;
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
    public class EfUserRentingsQuery : EfUseCase, IFindUserRentingsQuery
    {
        private IApplicationUser _user;
        public EfUserRentingsQuery(CarRentalContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 17;

        public string Name => "Find user rentigs";

        public string Description => "Find User rentings";

        public IEnumerable<RentingsQueryDto> Execute(DummyClass request)
        {
            var rentings = Context.Rentings.Include(x => x.Car)
                                           .Include(x => x.Car).ThenInclude(y => y.Model)
                                           .Include(x => x.Car).ThenInclude(y => y.Model).ThenInclude(y => y.Manufacturer)
                                           .Where(x => x.UserId == _user.Id && x.DeletedAt == null && x.StartDate>= DateTime.UtcNow);

            if(rentings == null)
            {
                throw new EntityNotFoundException("Rentings", _user.Id);
            }

            var result = rentings.Select(x => new RentingsQueryDto
            {
                Id = x.Id,
                StartOfRent = x.StartDate,
                EndOfRent = x.EndtDate,
                SumCost = x.SumCost,
                RentAddress = x.RentAdress,
                Model = x.Car.Model.ModelName,
                Manufacturer = x.Car.Model.Manufacturer.Manufacturer_Name
            }).ToList();

            return result;
                                            
        }
    }
}
