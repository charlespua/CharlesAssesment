using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CharlesAssesment.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;

        public TokenController(ILogger<TokenController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Login to get authentication token
        /// </summary>DD
        /// <returns code="200">Returns a token for authentication</returns>
        /// <returns code="500">Something is wrong</returns>
        [HttpGet]
        [ApiVersion("1.0")]
        public IActionResult Get(string username, string password)
        {
            try
            {
                var jwtKey = "c670c5d6c17a791d80e5342bdd27147f4af0a87753f167066ab979f25f5e0022";

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "CWP",
                    Audience = "CWP",
                    Subject = new ClaimsIdentity(new[]
                        {
                        new Claim("UserName", username),
                        new Claim("Name", "James Lim"),
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                _logger.LogInformation($"Generated new token - {token}");
                return Ok(token);
            }
            catch
            {
                return StatusCode(500, "Something is wrong with token generation");
            }
        }
    }
}