using Sintra.Application.Features.Regions.Commands.UpdateRegionUsers;
using Sintra.Application.Features.Regions.Queries.GetRegionUsers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IRegionRepository : IGenericRepositoryAsync<Region>
    {
        Task<RegionDetailsModel> GetRegionUsers(int regionId);
        void EditRegionUsers(UpdateRegionUsersCommand model);
    }
}
