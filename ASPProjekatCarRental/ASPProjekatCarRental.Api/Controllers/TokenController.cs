using ASPProjekatCarRental.Api.Core;
using ASPProjekatCarRental.DataAccess;
using ASPProjekatCarRental.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPProjekatCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;
        private CarRentalContext _context; 

        public TokenController(JwtManager manager, CarRentalContext contex)
        {
            _manager = manager;
            _context = contex;
        }

        public class AuthenticatedResponse
        {
            public string? Token { get; set; }
            public string? RefreshToken { get; set; }
        }

        /// <summary>
        /// Returns JWT token
        /// </summary>
        /// <remarks>
        /// Endpoint returns Authorization (JWT) token.
        /// </remarks>  
        /// <response code="200">Successfully return token</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Server Error</response>

        // POST api/<TokenController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] TokenRequest request)
        {
            try
            {
                var token = _manager.MakeInitalToken(request.Email, request.Password);
                var refresh = _manager.GenerateRefreshToken();

                var user = new LoginModel
                {
                    Email = request.Email,
                    RefreshToken = refresh,
                    RefreshTokenExpiryTime = DateTime.Now.AddMinutes(60)
                };

                _context.LoginModels.Add(user);
                _context.SaveChanges();

                return Ok(new { token, refresh });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public class TokenApiModel
        {
            public string? token { get; set; }
            public string? refresh { get; set; }
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel, [FromServices] ITokenStorage storage)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.token;
            string refreshToken = tokenApiModel.refresh;

            var principal = _manager.GetPrincipalFromExpiredToken(accessToken);
            var identity = principal.Identity as ClaimsIdentity;
            var email = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                email = identity.FindFirst("Email").Value;

            }
            var user = _context.LoginModels.OrderByDescending(x=> x.Id).FirstOrDefault(u => u.Email == email);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var token = _manager.MakeRefreshToken(email);
            var refresh = _manager.GenerateRefreshToken();

            user.RefreshToken = refresh;
            _context.SaveChanges();

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(accessToken);

            string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;

            storage.InvalidateToken(jti);

            return Ok(new { token, refresh });
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <remarks>
        /// This command logging out user.
        /// </remarks>  
        /// <response code="204">Successfully deleted car</response>
        /// <response code="500">Server Error</response>

        [HttpDelete]
        [Authorize]
        public IActionResult InvalidateToken([FromServices] ITokenStorage storage)
        {
            var header = HttpContext.Request.Headers["Authorization"];

            var token = header.ToString().Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;

            storage.InvalidateToken(jti);

            return NoContent();
        }

    }

    

    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
