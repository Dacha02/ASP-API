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
    public class PricesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PricesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
/*
        // GET: api/<PricesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PricesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        /// <summary>
        /// Inserting new price
        /// </summary>
        /// <remarks>
        /// This methos is inserting new price
        /// </remarks>  
        /// <response code="204">Successfully inserted price</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // POST api/<PricesController>
        [HttpPost]
        public IActionResult Post([FromBody] ReceivePriceDto dto, [FromServices] ICreatePriceDto command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

       /* // PUT api/<PricesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PricesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
