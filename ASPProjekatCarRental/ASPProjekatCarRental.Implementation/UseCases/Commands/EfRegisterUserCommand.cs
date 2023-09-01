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
using static System.Net.Mime.MediaTypeNames;

namespace ASPProjekatCarRental.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        public EfRegisterUserCommand(CarRentalContext context, RegisterUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Register User command";

        public string Description => "Command for registering user";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var cryptedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Password = cryptedPassword,
                Phone = request.Phone,
                Adress = request.Address,
                ImagePath = request.ImagePath
            };

            var userUseCases = new List<UserUseCase>
            {
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 3
                },
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 5
                },
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 11
                },
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 20
                },
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 17
                },
                new UserUseCase
                {
                    User = user,
                    UseCaseId = 16
                },

                new UserUseCase
                {
                    User = user,
                    UseCaseId = 14
                }
            };

            Context.Users.Add(user);
            Context.UserUseCases.AddRange(userUseCases);
            Context.SaveChanges();

        }
    }
}
