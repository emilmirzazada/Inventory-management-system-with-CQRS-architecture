using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.StartupInjections.Authorization
{
    public class GlobalAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authService;

        public GlobalAuthorizeFilter(IAuthorizationService authorizationService)
        {
            authService = authorizationService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationPolicyBuilder = new AuthorizationPolicyBuilder()
                                                    .RequireAuthenticatedUser();
            var name = context.ActionDescriptor.RouteValues.First().Value;
            var res = await authService.AuthorizeAsync(context.HttpContext.User, authorizationPolicyBuilder.Build());
            if (!res.Succeeded && !(name == "ResetPassword" ||
                name == "ForgotPassword" ||
                name == "Register" ||
                name == "Login"))
            {
                context.Result = new RedirectResult("/Account/Login");
            }

        }
    }
}
