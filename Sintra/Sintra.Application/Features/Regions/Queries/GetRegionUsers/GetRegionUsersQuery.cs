using MediatR;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Queries.GetRegionUsers
{
    public class GetRegionUsersQuery : IRequest<RegionDetailsModel>
    {
        public int Id { get; set; }
        public class GetRegionUsersQueryHandler : IRequestHandler<GetRegionUsersQuery, RegionDetailsModel>
        {
            private readonly IRegionRepository _RegionRepository;
            public GetRegionUsersQueryHandler(IRegionRepository RegionRepository)
            {
                _RegionRepository = RegionRepository;
            }
            public async Task<RegionDetailsModel> Handle(GetRegionUsersQuery query, CancellationToken cancellationToken)
            {
                var regionUsers = await _RegionRepository.GetRegionUsers(query.Id);
                if (regionUsers == null) throw new Exception($"Region Not Found.");
                return regionUsers;
            }
        }
    }
}
