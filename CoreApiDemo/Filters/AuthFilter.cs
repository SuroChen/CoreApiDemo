using CoreApiDemo.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace CoreApiDemo.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (context.Filters.Any(item => item is IAllowAnonymousFilter)) return;

            if (auth != null)
            {
                var result = JwtUtil.verifyTokenByBuilder(auth);
                if (result != "1")
                {
                    context.Result = new JsonResult(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                context.Result = new JsonResult(HttpStatusCode.Unauthorized);
            }
        }
    }
}
