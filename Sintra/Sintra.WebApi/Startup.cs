using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sintra.Application;
using Sintra.Application.Interfaces;
using Sintra.Infrastructure.Persistence;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Settings;
using Sintra.Infrastructure.Shared;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.Services;
using Sintra.WebApi.StartupInjections;

namespace Sintra.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<MyApiAuthFilter>();
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddApiAuthentification(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddPersistenceApiServices(_config);
            services.AddSharedInfrastructure(_config);
            services.AddSwaggerExtension();
            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            

            services.Configure<WebAdminAppSettings>(_config.GetSection(nameof(WebAdminAppSettings)));
            //Explicitly register the settings object by delegating to the IOptions object so that it can be accessed globally via AppServicesHelper.
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<WebAdminAppSettings>>().CurrentValue);

            services.AddScoped<IDapper, DapperClass>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            AppServicesHelper.Services = app.ApplicationServices;

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            
            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseSwaggerExtension();
            
            app.UseErrorHandlingMiddleware();
            
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }
    }
}
