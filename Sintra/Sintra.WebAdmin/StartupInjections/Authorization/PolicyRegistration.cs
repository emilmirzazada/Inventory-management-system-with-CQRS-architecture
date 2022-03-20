using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.StartupInjections.Authorization
{
    public static class PolicyRegistration
    {
        public static void AddUserPolicies(this IServiceCollection services)
        {
            services.AddControllersWithViews(config =>
            {
                config.Filters.Add(services.BuildServiceProvider().GetRequiredService<GlobalAuthorizeFilter>());
            }).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/home/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,
                    Name = ".Sintra.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });
        }
    }
}
