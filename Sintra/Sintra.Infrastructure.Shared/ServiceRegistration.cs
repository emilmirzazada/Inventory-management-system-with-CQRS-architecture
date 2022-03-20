using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sintra.Application.Interfaces;
using Sintra.Domain.Settings;
using Sintra.Infrastructure.Shared.Services;

namespace Sintra.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
