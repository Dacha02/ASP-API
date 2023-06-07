using ASPProjekatCarRental.Api.Core.Dto;
using ASPProjekatCarRental.Application.UseCases.Commands;
using ASPProjekatCarRental.Application.UseCases.DTO.ReceiveDto;
using ASPProjekatCarRental.Application.UseCases.DTO.SearchDto;
using ASPProjekatCarRental.Application.UseCases.Queries;
using ASPProjekatCarRental.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ASPProjekatCarRental.Application.UseCases.DTO.SearchDto.PagedSearch;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensions =>
            new List<string> { ".jpg", ".png", ".jpeg" };

        private UseCaseHandler _handler;

        public CarController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Return paged cars response
        /// </summary>
        /// <remarks>
        /// Query params:
        /// <b>- Keyword:</b> Filter cars by Manufacturer, Model, and Registration plate.
        /// <b>- StartOfRent and EndOfRent:</b> Only if those two are passed it will filter out cars that are available in that period of time.
        /// <b>- PerPage</b> Return number of cars that are set by PerPage
        /// <b>- Page</b> Return cars on that page
        /// 
        /// Returns:<br/>
        /// <b>Manufacturer name</b><br/>
        /// <b>Model name</b><br/>
        /// <b>Registration plate</b><br/>
        /// <b>Car category</b><br/>
        /// <b>Start date of registraion</b><br/>
        /// <b>End date of registraion</b><br/>
        /// <b>Price for renting car for a day</b><br/>
        /// <b>Price for renting car for a month</b><br/>
        /// <b>Car specifiactions</b>
        /// </remarks>  
        /// <response code="200">Successfully returns cars</response>
        /// <response code="500">Server Error</response>

        // GET: api/<CarController>
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearchWithIsRented dto, [FromServices] IGetCarsQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        /// <summary>
        /// Return car with specified Id
        /// </summary>
        /// <remarks>
        /// Returns:<br/>
        /// <b>Manufacturer name</b><br/>
        /// <b>Model name</b><br/>
        /// <b>Registration plate</b><br/>
        /// <b>Car category</b><br/>
        /// <b>Start date of registraion</b><br/>
        /// <b>End date of registraion</b><br/>
        /// <b>Price for renting car for a day</b><br/>
        /// <b>Price for renting car for a month</b><br/>
        /// <b>Car specifiactions</b>
        /// </remarks>  
        /// <response code="200">Successfully returns cars</response>
        /// <response code="500">Server Error</response>

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindCarQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Inserting cars
        /// </summary>
        /// <remarks>
        /// This methos is inserting new car
        /// </remarks>  
        /// <response code="204">Successfully inserted car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // POST api/<CarController>
        [HttpPost]
        public IActionResult Post([FromForm] ReciveCarWithImageDto dto, [FromServices] ICreateCarCommand command)
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
                var filePath = Path.Combine("wwwroot", "carsimages", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.Image.CopyTo(stream);

                dto.ImagePath = fileName;
            }

            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        /// <summary>
        /// Changing car informations
        /// </summary>
        /// <remarks>
        /// This method change already existed values for a car, except the price (it is adding new price because we need history of past prices)
        /// </remarks>  
        /// <response code="204">Successfully changed car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // PUT api/<CarController>/5
        [HttpPut]
        public IActionResult Put([FromForm] ReciveCarWithCarIdWithImageDto dto, [FromServices] IChangeCarCommand command)
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
                var filePath = Path.Combine("wwwroot", "carsimages", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.Image.CopyTo(stream);

                dto.ImagePath = fileName;
            }

            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        /// <summary>
        /// Deleting car
        /// </summary>
        /// <remarks>
        /// This command is setting DeletedAt property to current date. It's not allowed to fully delete car!
        /// </remarks>  
        /// <response code="204">Successfully deleted car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCarCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
