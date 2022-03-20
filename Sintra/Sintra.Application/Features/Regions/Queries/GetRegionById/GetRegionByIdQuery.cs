using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Queries.GetRegionById
{
    public class GetRegionByIdQuery : IRequest<Response<Region>>
    {
        public int Id { get; set; }
        public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, Response<Region>>
        {
            private readonly IRegionRepository _RegionRepository;
            public GetRegionByIdQueryHandler(IRegionRepository RegionRepository)
            {
                _RegionRepository = RegionRepository;
            }
            public async Task<Response<Region>> Handle(GetRegionByIdQuery query, CancellationToken cancellationToken)
            {
                var Region = await _RegionRepository.GetByIdAsync(query.Id);
                if (Region == null) throw new Exception($"Region Not Found.");
                return new Response<Region>(Region);
            }
        }
    }
}
