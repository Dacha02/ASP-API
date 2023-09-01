using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? ImagePath { get; set; }
    }

    public class UpdateUserDto: RegisterUserDto
    {
        public int UserId { get; set; }
    }
}
