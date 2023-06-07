using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(CarRentalContext _contex)
        {
            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            var userNameRegex = @"^(?=[a-zA-Z0-9._]{3,12}$)(?!.*[_.]{2})[^_.].*[^_.]$";
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            
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
                   .MaximumLength(12).WithMessage("Maximum length 12 characters!")
                   .Matches(userNameRegex).WithMessage("Username is not in correct format!")
                   .Must(x => !_contex.Users.Any(u => u.UserName == x)).WithMessage("Username {PropertyValue} is already in use!");

            RuleFor(x => x.Password)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty().WithMessage("Password is required!")
                   .MinimumLength(8).WithMessage("Minimum length is 8 characters!")
                   .Matches(passwordRegex).WithMessage("Password is not in correct format!");

            RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Email is required!")
                    .EmailAddress().WithMessage("Email is not in correct format!")
                    .Must(x => !_contex.Users.Any(e => e.Email == x)).WithMessage("Email is already in use, please login!");

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
        }
    }
}
