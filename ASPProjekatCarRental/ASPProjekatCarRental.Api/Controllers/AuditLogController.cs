using ASPProjekatCarRental.Application.UseCases.Commands;
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
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private UseCaseHandler _handler;

        public AuditLogController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Return paged audit log response
        /// </summary>
        /// <remarks>
        /// Query params:
        /// <b>- Keyword:</b> Filter audit log by Use Case name and Username.
        /// <b>- DateFrom:</b> Filter audit log by date greater than provided date
        /// <b>- DateTo:</b> Filter audit log by date lower than provided date
        /// 
        /// Returns:
        /// <b>Username of the User who did something on the system</b>
        /// <b>Use Case name</b>
        /// <b>Date and time od execution</b>
        /// <b>Data that is returned or sent</b>
        /// <b>IsAuthorized if he is authorized to do something return true otherwise return false</b>
        /// </remarks>  
        /// <response code="200">Successfully returns audit log information</response>
        /// <response code="500">Server Error</response>

        // GET: api/<AuditLogController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchForAuditLog dto, [FromServices] IGetAuditLogs query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

    }
}
