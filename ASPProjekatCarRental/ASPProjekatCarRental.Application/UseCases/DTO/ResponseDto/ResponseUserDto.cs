using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekatCarRental.Application.UseCases.DTO.ResponseDto
{
    public class ResponseUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DeletedAt { get; set; }

        public IEnumerable<UserUseCasesDto> UserUseCases { get; set; } = new List<UserUseCasesDto>();
    }

    public class UserUseCasesDto
    {
        public string UseCaseName { get; set; }
    }
}
