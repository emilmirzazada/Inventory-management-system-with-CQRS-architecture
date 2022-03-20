using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Regions.Commands.UpdateRegionUsers;
using Sintra.Application.Features.Regions.Queries.GetRegionUsers;
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
    public class RegionRepository : GenericRepositoryAsync<Region>, IRegionRepository
    {
        private readonly DbSet<Region> regions;
        private readonly IdentityContext ctx;
        private readonly UserManager<ApplicationUser> userManager;

        public RegionRepository(IdentityContext dbContext,
            UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            regions = dbContext.Set<Region>();
            this.ctx = dbContext;
            this.userManager = userManager;
        }

        public async Task<RegionDetailsModel> GetRegionUsers(int regionId)
        {
            var region = await regions.FirstOrDefaultAsync(x => x.Id == regionId);
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();
            var userregions = ctx.UserRegions
                                .Include(x => x.Region)
                                .Include(x => x.User)
                                .Where(x=>x.RegionId==regionId)
                                .ToList();
            foreach (var user in userManager.Users)
            {
                var list = userregions.Any(x=>x.UserId==user.Id) ? members : nonmembers;
                list.Add(user);
            }
            var model = new RegionDetailsModel()
            {
                Region = region,
                Members = members,
                NonMembers = nonmembers
            };
            return model;
        }

        public void EditRegionUsers(UpdateRegionUsersCommand model)
        {
            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var userRegion = new UserRegion
                {
                    UserId =userId,
                    RegionId=model.RegionId
                };
                ctx.UserRegions.Add(userRegion);
                ctx.SaveChanges();
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var userRegions = ctx.UserRegions.Where(x => x.UserId == userId && x.RegionId == model.RegionId).ToList();
                ctx.UserRegions.RemoveRange(userRegions);
                ctx.SaveChanges();
            }

        }
    }
}
