using ASPProjekatCarRental.Api.Core.Dto;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensions =>
            new List<string> { ".jpg", ".png", ".jpeg" };

        private UseCaseHandler _handler;
        public RegisterController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Inserting new User
        /// </summary>
        /// <remarks>
        /// For this method you don't need to be authorized to execute
        /// </remarks>  
        /// <response code="204">Successfully inserted user</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromForm] RegisterUserWithImageDto dto, [FromServices] IRegisterUserCommand command)
        {

            if(dto.Image != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(dto.Image.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsupported file type.");
                }

                var fileName = guid + extension;
                var filePath = Path.Combine("wwwroot", "userimages", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.Image.CopyTo(stream);

                dto.ImagePath = fileName;
            }

            _handler.HandleCommand(command, dto);
            return Ok();
        }

    }
}
