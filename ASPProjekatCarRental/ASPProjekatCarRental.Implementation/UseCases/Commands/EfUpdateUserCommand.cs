using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Implementation.Validators;
using Faker;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validator;
        public EfUpdateUserCommand(CarRentalContext context, UpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Update User Command";

        public string Description => "Command for updating user data";

        public void Execute(UpdateUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var cryptedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = Context.Users.Find(request.UserId);

            user.FirstName = request.FirstName;
            user.UserName = request.Username;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = cryptedPassword;
            user.Adress = request.Address;
            user.Phone = request.Phone;
            user.ImagePath = request.ImagePath;

            Context.SaveChanges();
        }
    }
}
