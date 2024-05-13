using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Entities.Entities;

namespace EmailProviderSystem.Web.APIs.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {

        private ITokenService _tokenService;

        public AuthenticationFilter(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                authHeader = authHeader.Replace("Bearer ", "");

                if (!(_tokenService.ValidateToken(authHeader)))
                {
                    context.Result = new UnauthorizedObjectResult(new { message = "Invalid token" });
                }
                else
                {
                    User user = _tokenService.GetUserFromToken(authHeader);

                    context.HttpContext.Items["Email"] = user.Email;
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Authorization header is missing" });
            }

            base.OnActionExecuting(context);
        }
    }
}
