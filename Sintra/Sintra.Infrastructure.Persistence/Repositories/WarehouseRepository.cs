using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseUsers;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseUsers;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepository : GenericRepositoryAsync<Warehouse>, IWarehouseRepository
    {
        private readonly DbSet<Warehouse> warehouses;
        private readonly IdentityContext ctx;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public WarehouseRepository(IdentityContext dbContext, IMapper mapper,
            UserManager<ApplicationUser> userManager) : base(dbContext, mapper)
        {
            warehouses = dbContext.Set<Warehouse>();
            this.ctx = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public List<GetAllWarehousesViewModel> GetWarehouses(string userId)
        {
            List<WarehouseUser> warehouseUser = ctx.WarehouseUsers.Where(x => x.UserId == userId).ToList();
            if (userId == "55429e65-4850-4642-a89d-bfadbea02db1")
                return Get<Warehouse, GetAllWarehousesViewModel>(x => x.Deleted == false);
            else
                return Get<Warehouse, GetAllWarehousesViewModel>(x => x.Deleted == false && warehouseUser.Select(y => y.WarehouseId).Contains(x.Id));
        }
        public List<Warehouse> GetMobileWarehouses(string userId)
        {
            List<WarehouseUser> warehouseUser = ctx.WarehouseUsers.Where(x => x.UserId == userId).ToList();
            if (userId == "55429e65-4850-4642-a89d-bfadbea02db1")
                return Get<Warehouse,Warehouse>(x => x.Deleted == false);
            else
                return Get<Warehouse,Warehouse>(x => x.Deleted == false && warehouseUser.Select(y => y.WarehouseId).Contains(x.Id));
        }
        public async Task<WarehouseUsersModel> GetWarehouseUsers(int warehouseId)
        {
            var warehouse = await warehouses.FirstOrDefaultAsync(x => x.Id == warehouseId);
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();
            var warehouseUsers = ctx.WarehouseUsers
                                .Include(x => x.Warehouse)
                                .Include(x => x.User)
                                .Where(x => x.WarehouseId == warehouseId)
                                .ToList();
            foreach (var user in userManager.Users)
            {
                var list = warehouseUsers.Any(x => x.UserId == user.Id) ? members : nonmembers;
                list.Add(user);
            }
            var model = new WarehouseUsersModel()
            {
                Warehouse = warehouse,
                Members = members,
                NonMembers = nonmembers
            };
            return model;
        }

        public void EditWarehouseUsers(UpdateWarehouseUsersCommand model)
        {
            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var warehouseUser = new WarehouseUser
                {
                    UserId = userId,
                    WarehouseId = model.WarehouseId
                };
                ctx.WarehouseUsers.Add(warehouseUser);
                ctx.SaveChanges();
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var warehouseUsers = ctx.WarehouseUsers.Where(x => x.UserId == userId && x.WarehouseId == model.WarehouseId).ToList();
                ctx.WarehouseUsers.RemoveRange(warehouseUsers);
                ctx.SaveChanges();
            }

        }
    }
}
