using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator(CarRentalContext _context)
        {
            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            var userNameRegex = @"^(?=[a-zA-Z0-9._]{3,12}$)(?!.*[_.]{2})[^_.].*[^_.]$";
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            RuleFor(x => x.UserId)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("UserId is required!")
                   .Must(x => _context.Users.Any(y => y.Id == x)).WithMessage("User with id: {PropertyValue} doesn't exist");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First name is required!")
                .Matches(imePrezimeRegex).WithMessage("First name is not in correct format!");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last name is required!")
                .Matches(imePrezimeRegex).WithMessage("Last name is not in correct format!");

            RuleFor(x => x.Username)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Username is required!")
                    .MinimumLength(3).WithMessage("Minimum length is 3 characters!")
                    .MaximumLength(12).WithMessage("Maximum length is 12 characters!")
                    .Matches(userNameRegex).WithMessage("Username is not in the correct format!");

          /*  RuleFor(x => x.Password)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Password is required!")
                   .MinimumLength(8).WithMessage("Minimum length is 8 characters!")
                   .Matches(passwordRegex).WithMessage("Password is not in correct format!");

            RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Email is required!")
                    .EmailAddress().WithMessage("Email is not in correct format!");*/

            RuleFor(x => x.Phone)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Phone is required!")
                   .MinimumLength(9).WithMessage("Minimum phone length is 9 numbers");

            RuleFor(x => x.Address)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Adrdress is required!");

            RuleFor(x => x.ImagePath)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Image path is required!");

            RuleFor(x=> x)
                   .Must(x => CheckUsername(x.Username, x.UserId, _context)).WithMessage("Usrname is already in use")
                   .Must(x => CheckEmail(x.Email, x.UserId, _context)).WithMessage("Email is already in use");

        }

        private bool CheckUsername(string username, int userId, CarRentalContext context)
        {
            return !context.Users.Any(x => x.UserName == username && x.Id != userId);
        }

        private bool CheckEmail(string email, int userId, CarRentalContext context)
        {
            return !context.Users.Any(x => x.Email == email && x.Id != userId);
        }
    }
}
