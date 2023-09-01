using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivateUserController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public ActivateUserController(UseCaseHandler handler)
        {
            _useCaseHandler = handler;
        }
        // PUT api/<ActivateUserController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ReceiveUserIdDto id, [FromServices] IActivateUserCommand command)
        {
            _useCaseHandler.HandleCommand(command, id);
            return NoContent();
        }

    }
}
