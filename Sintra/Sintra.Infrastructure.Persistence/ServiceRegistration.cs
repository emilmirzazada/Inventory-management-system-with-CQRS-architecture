using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Repositories;
using Sintra.Infrastructure.Persistence.Repository;
using Sintra.Infrastructure.Persistence.Settings;

namespace Sintra.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
           options.UseSqlServer(AppServicesHelper.Config.ConnectionString,
               b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
        }
        #region ApiRepositories
        public static void AddPersistenceApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IWarehouseProductRepository, WarehouseProductRepository>();
            services.AddTransient<IProductAccessoryRepository, ProductAccessoryRepository>();
            services.AddTransient<ICategoryAccessoryRepository, CategoryAccessoryRepository>();
            services.AddTransient<IProductTransferRepository, ProductTransferRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICreditRepository, CreditRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ICreditCallRepository, CreditCallRepository>();
            services.AddTransient<IExpirationCallRepository, ExpirationCallRepository>();
            services.AddTransient<IOrderBonusRepository, OrderBonusRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IReceipeRepository, RecipeRepository>();
        }
        #endregion
        #region AdminRepositories
        public static void AddPersistenceAdminServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IWarehouseProductRepository, WarehouseProductRepository>();
            services.AddTransient<IProductAccessoryRepository, ProductAccessoryRepository>();
            services.AddTransient<ICategoryAccessoryRepository, CategoryAccessoryRepository>();
            services.AddTransient<IProductTransferRepository, ProductTransferRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICreditRepository, CreditRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ICreditCallRepository, CreditCallRepository>();
            services.AddTransient<IExpirationCallRepository, ExpirationCallRepository>();
            services.AddTransient<IOrderBonusRepository, OrderBonusRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IReceipeRepository, RecipeRepository>();
        }
        #endregion
    }
}
