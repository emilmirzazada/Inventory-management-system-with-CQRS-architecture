using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintra.Infrastructure.Persistence.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAccessory> CategoryAccessories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductAccessory> ProductAccessories { get; set; }
        public DbSet<ProductTransfer> ProductTransfers { get; set; }
        public DbSet<TransferProducts> TransferProducts { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBonus> OrderBonuses { get; set; }
        public DbSet<OrderAccessory> OrderAccessories { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserRegion> UserRegions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public DbSet<WarehouseUser> WarehouseUsers { get; set; }

        public DbSet<CreditCall> CreditCalls { get; set; }

        public DbSet<ExpirationCall> ExpirationCalls { get; set; }

        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<EmployeeBalanceTransaction> EmployeeBalanceTransactions { get; set; }
        public DbSet<EmployeeBonusTransaction> EmployeeBonusTransactions { get; set; }
        public DbSet<EmployeeCreditBalanceTransaction> EmployeeCreditBalanceTransactions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Receipe> Receipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasKey(x => new { x.ProductId, x.CategoryId });
            builder.Entity<ProductAccessory>()
                .HasKey(x => new { x.ParentId, x.AccessoryId });
            builder.Entity<UserRegion>()
                .HasKey(x => new { x.UserId, x.RegionId });
            builder.Entity<WarehouseProduct>()
                .HasKey(x => new { x.WarehouseId, x.ProductId });
            builder.Entity<WarehouseUser>()
                .HasKey(x => new { x.WarehouseId, x.UserId });
            builder.Entity<TransferProducts>()
                .HasKey(x => new { x.TransferId, x.ProductId });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }



            //All Decimals will have 18,2 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }


            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole<string>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
