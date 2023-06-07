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
    public class DiscountController : ControllerBase
    {
        private UseCaseHandler _handler;

        public DiscountController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        /*
        // GET: api/<DiscountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DiscountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DiscountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }*/


        /// <summary>
        /// Changing discount informations
        /// </summary>
        /// <remarks>
        /// This method change already existed values for a discount
        /// </remarks>  
        /// <response code="204">Successfully changed discount</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // PUT api/<DiscountController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ReceiveDiscountDto dto, [FromServices] IPutDiscountCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        /*// DELETE api/<DiscountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
