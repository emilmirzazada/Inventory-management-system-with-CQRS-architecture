using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sintra.Application;
using Sintra.Infrastructure.Persistence;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Settings;
using Sintra.Infrastructure.Shared;
using Sintra.WebAdmin.StartupInjections.Authorization;

namespace Sintra.WebAdmin
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<GlobalAuthorizeFilter>();
            services.AddScoped<MyAuthFilter>();
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(Configuration);
            services.AddWebAuthentification(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddPersistenceAdminServices(Configuration);
            services.AddSharedInfrastructure(Configuration);

            services.Configure<WebAdminAppSettings>(Configuration.GetSection(nameof(WebAdminAppSettings)));
            //Explicitly register the settings object by delegating to the IOptions object so that it can be accessed globally via AppServicesHelper.
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<WebAdminAppSettings>>().CurrentValue);

            services.AddScoped<IDapper, DapperClass>();

            services.AddUserPolicies();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            AppServicesHelper.Services = app.ApplicationServices;

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
