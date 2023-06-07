﻿using ASPProjekatCarRental.Application.UseCases.Commands;
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
    public class RentingsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public RentingsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /*// GET: api/<RentingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RentingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        /// <summary>
        /// Renting cars
        /// </summary>
        /// <remarks>
        /// This method is for renting a car. You can't rent car that is already rented and can't rent car which property DeletedAt is not null.
        /// </remarks>  
        /// <response code="201">Successfully rented a car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // POST api/<RentingsController>
        [HttpPost]
        public IActionResult Post([FromBody] ReceiveRentingDto dto, [FromServices] ICreateRentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        /*// PUT api/<RentingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        /// <summary>
        /// Deleting rent
        /// </summary>
        /// <remarks>
        /// This command is setting DeletedAt property to current date. It's not allowed to fully rent!
        /// </remarks>  
        /// <response code="204">Successfully deleted car</response>
        /// <response code="400">Validation exception</response>
        /// <response code="500">Server Error</response>

        // DELETE api/<RentingsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}