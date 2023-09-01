using ASPProjekatCarRental.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASPProjekatCarRental.Api.Core
{
    public class JwtManager
    {
        private readonly CarRentalContext _context;
        private readonly JwtSettings _settings;
        private readonly ITokenStorage _storage;


        public JwtManager(CarRentalContext context, JwtSettings settings, ITokenStorage storage)
        {
            _context = context;
            _settings = settings;
            _storage = storage;
        }

        public string MakeInitalToken(string email, string password)
        {
            var user = _context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Email == email);
            
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!valid)
            {
                throw new UnauthorizedAccessException();
            }

            var actor = new JwtUser
            {
                Id = user.Id,
                UseCaseIds = user.UserUseCases.Select(x => x.UseCaseId).ToList(),
                Identity = user.Email,
                Email = user.Email
            };

            var tokenId = Guid.NewGuid().ToString();
            _storage.AddToken(tokenId);

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("UseCases", JsonConvert.SerializeObject(actor.UseCaseIds)),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string MakeRefreshToken(string email)
        {
            var user = _context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var actor = new JwtUser
            {
                Id = user.Id,
                UseCaseIds = user.UserUseCases.Select(x => x.UseCaseId).ToList(),
                Identity = user.Email,
                Email = user.Email
            };

            var tokenId = Guid.NewGuid().ToString();
            _storage.AddToken(tokenId);

            var claims = new List<Claim> // Jti : "",
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("UseCases", JsonConvert.SerializeObject(actor.UseCaseIds)),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123ASDjfipoawopriqwop123123")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

    }
}
