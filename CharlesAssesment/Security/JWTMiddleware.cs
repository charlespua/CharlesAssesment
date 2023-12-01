using CharlesAssesment.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CharlesAssesment.Security
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        
        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Just check if token is generated (oversimplification)
            if (token != null )
            {
                var account = ValidateJwtToken(token);
                if (account != null)
                    context.Items["Account"] = account;
            }

            await _next(context);
        }

        public Account ValidateJwtToken(string token)
        {
            var jwtKey = "c670c5d6c17a791d80e5342bdd27147f4af0a87753f167066ab979f25f5e0022";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "CWP",
                    ValidAudience = "CWP",
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                //Get Payload Data
                var name = jwtToken.Claims.First(x => x.Type == "Name").Value;

                //Get User by email
                var account = new Account
                {
                    Name = jwtToken.Claims.First(x => x.Type == "Name").Value,
                    Username = jwtToken.Claims.First(x => x.Type == "UserName").Value
                };

                // return User object from JWT token if validation successful
                return account;
            }
            catch (Exception ex)
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
