using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.StartupInjections.Authorization
{
    public class MyAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authService;

        public MyAuthFilter(IAuthorizationService authorizationService)
        {
            authService = authorizationService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            HttpMethodAttribute httpmethod = (HttpMethodAttribute)context.ActionDescriptor.EndpointMetadata.First(x => x.GetType().BaseType == typeof(HttpMethodAttribute));
            string claimName = httpmethod.Name;


            var authorizationPolicyBuilder =
                new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);
            authorizationPolicyBuilder.RequireClaim(claimName);

            var res = await authService.AuthorizeAsync(context.HttpContext.User, authorizationPolicyBuilder.Build());
            if (!res.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
