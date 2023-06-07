using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;

namespace ASPProjekatCarRental.Api.Core.Dto
{
    public class RegisterUserWithImageDto : RegisterUserDto
    {
        public IFormFile Image { get; set; }
    }

    public class UpdateUserWithImageDto: UpdateUserDto
    {
        public IFormFile Image { get; set; }
    }
}
