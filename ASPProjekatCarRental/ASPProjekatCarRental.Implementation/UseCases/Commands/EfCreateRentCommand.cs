using ASPProjekatCarRental.Application.Emails;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
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
    public class EfCreateRentCommand : EfUseCase, ICreateRentCommand
    {
        private readonly InsertRentValidator _validator;
        private IApplicationUser _user;
        private readonly IEmailSender _sender;
        public EfCreateRentCommand(CarRentalContext context, InsertRentValidator validator, IEmailSender sender, IApplicationUser user) : base(context)
        {
            _validator = validator;
            _sender = sender;
            _user = user;
        }

        public int Id => 5;

        public string Name => "Creating Rent data";

        public string Description => "Command for creating rent's";

        public void Execute(ReceiveRentingDto request)
        {
            //Treba proveriti da li postoji auto i korisnik sa zadatim ID-jem
            //Treba proveriti da li je auto slobodan u prosledjenom terminu
            //Treba zabraniti da se prosledi EndDate veci od StartDate
            //Treba izracunati Sumu na osnovu toga na koliko dugo je auto iznajmljen

            _validator.ValidateAndThrow(request);

            TimeSpan duration = request.EndDate - request.StartDate;
            decimal sumCost;

            decimal perDay = Context.Prices.Where(x => x.CarId == request.CarId)
                                          .OrderByDescending(x => x.CreatedAt)
                                          .Select(x => x.PricePerDay)
                                          .FirstOrDefault();

            decimal perMonth = Context.Prices.Where(x => x.CarId == request.CarId)
                                            .OrderByDescending(x => x.CreatedAt)
                                            .Select(x => x.PricePerMonth)
                                            .FirstOrDefault();

            var carrr = Context.Cars.FirstOrDefault(x => x.Id == request.CarId);

            var categoryId = Context.Categories.Where(x => x.Id == carrr.CategoryId).Select(x=> x.Id).FirstOrDefault();

            int dicount = Context.Discounts.Where(x => x.CategoryId == categoryId).OrderByDescending(x=> x.CreatedAt).Select(x => x.Percentage).FirstOrDefault();

            

            if (duration.TotalDays >= 30)
            {
                int months = (int)duration.TotalDays / 30;
                sumCost = perMonth * months;
            }
            else
            {
                int days = (int)duration.TotalDays;
                sumCost = perDay * days;
            }

            // Handle case when duration is less than a day
            if (duration.TotalDays < 1 && duration.TotalHours > 0)
            {
                sumCost = perDay;
            }

            if (dicount != 0)
            {
                sumCost = sumCost - ((dicount / 100m) * sumCost);
            }

            var rent = new Renting
            {
                UserId = _user.Id,
                CarId = request.CarId,
                StartDate = request.StartDate,
                EndtDate = request.EndDate,
                SumCost = sumCost,
                RentAdress = (request.RentAdress == null) ? Context.Rentings.Where(x => x.CarId == request.CarId).Select(x => x.RentAdress).First() : request.RentAdress,
                IsPaid = false
            };

            Context.Rentings.Add(rent);
            Context.SaveChanges();

            var user = Context.Users.Find(_user.Id);
            var car = Context.Cars.Find(request.CarId);
            var model = Context.Models.Find(car.Model.Id);
            var manufacturer = Context.Manufacturers.Find(model.ManufacturerId);

            _sender.Send(new MailMessageDto
            {
                To = user.Email,
                Title = "You successfuly rented a car",
                Body = "<html><body>" +
                       "<h2 style=\"font-weight: bold;\">Rent information:</h2>" +
                       $"<p><strong>Manufacturer:</strong> {manufacturer.Manufacturer_Name}</p>" +
                       $"<p><strong>Model:</strong> {model.ModelName}</p>" +
                       $"<p><strong>Rent Address:</strong> {rent.RentAdress}</p>" +
                       $"<p><strong>Start date:</strong> {rent.StartDate}</p>" +
                       $"<p><strong>End date:</strong> {rent.EndtDate}</p>" +
                       $"<p><strong>Cost:</strong> {rent.SumCost}e</p>" +
                       "</body></html>"
            });
        }
    }
}
