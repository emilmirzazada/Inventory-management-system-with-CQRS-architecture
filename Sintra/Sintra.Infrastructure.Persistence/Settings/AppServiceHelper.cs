using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;

namespace Sintra.Infrastructure.Persistence.Settings
{
    public static class AppServicesHelper
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext HttpContext_Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        public static IWebHostEnvironment HostingEnvironment
        {
            get
            {
                return services.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            }
        }

        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static WebAdminAppSettings Config
        {
            get
            {
                //This works to get file changes.
                var s = services.GetService(typeof(IOptionsMonitor<WebAdminAppSettings>)) as IOptionsMonitor<WebAdminAppSettings>;
                WebAdminAppSettings config = s.CurrentValue;

                return config;
            }
        }
    }
}
