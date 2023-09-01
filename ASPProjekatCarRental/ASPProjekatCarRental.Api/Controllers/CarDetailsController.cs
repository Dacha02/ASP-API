using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CarDetailsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        private DummyClass _class;

        // GET: api/<CarDetailsController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetCarDetailsQuery query)
        {
            return Ok(_handler.HandleQuery(query, _class));
        }

        
    }
}
