using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecificationController : ControllerBase
    {
        private UseCaseHandler _handler;

        public SpecificationController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /*// GET: api/<SpecificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SpecificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpecificationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpecificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
*/

        /// <summary>
        /// Deleting specification
        /// </summary>
        /// <remarks>
        /// This command is deleting specification from database.
        /// </remarks>  
        /// <response code="204">Successfully deleted specification</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // DELETE api/<SpecificationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecificationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
