using CharlesAssesment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CharlesAssesment.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JWTAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (Account)context.HttpContext.Items["Account"];

            if (account == null)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
