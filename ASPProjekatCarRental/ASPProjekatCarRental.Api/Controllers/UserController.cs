using ASPProjekatCarRental.Api.Core.Dto;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UserController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Return paged users response
        /// </summary>
        /// <remarks>
        /// Query params:
        /// <b>- Keyword:</b> Filter users by First name, Last name, Username and Email.
        /// <b>- PerPage</b> Return number of users that are set by PerPage
        /// <b>- Page</b> Return users on that page
        /// 
        /// Returns:<br/>
        /// <b>First name</b><br/>
        /// <b>Last name</b><br/>
        /// <b>Username</b><br/>
        /// <b>Email</b><br/>
        /// <b>Phone</b><br/>
        /// <b>Addres</b><br/>
        /// <b>Array of Use Cases</b><br/>
        /// </remarks>  
        /// <response code="200">Successfully returns cars</response>
        /// <response code="500">Server Error</response>

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearchDto dto, [FromServices] IGetUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        public static IEnumerable<string> AllowedExtensions =>
           new List<string> { ".jpg", ".png", ".jpeg" };

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Changing users informations
        /// </summary>
        /// <remarks>
        /// This method change already existed values for a user.
        /// </remarks>  
        /// <response code="204">Successfully changed car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // PUT api/<UserController>/5
        [HttpPut]
        public IActionResult Put([FromForm] UpdateUserWithImageDto dto, [FromServices] IUpdateUserCommand command)
        {
            if (dto.Image != null)
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

        /// <summary>
        /// Deleting user
        /// </summary>
        /// <remarks>
        /// This command is setting DeletedAt property to current date. It's not allowed to fully delete a user!
        /// </remarks>  
        /// <response code="204">Successfully deleted car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command)
        {
            _handler.HandleCommand(command, id);
            return StatusCode(204, new { message = "User succesuflly deleted!" });
        }
    }
}
