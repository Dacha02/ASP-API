using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;

namespace ASPProjekatCarRental.Api.Core.Dto
{
    public class ReciveCarWithImageDto : ReceiveCarDto
    {
        public IFormFile Image { get; set; }
    }

    public class ReciveCarWithCarIdWithImageDto : ReceiveCarDtoWithCarId
    {
        public IFormFile Image { get; set; }
    }
}
