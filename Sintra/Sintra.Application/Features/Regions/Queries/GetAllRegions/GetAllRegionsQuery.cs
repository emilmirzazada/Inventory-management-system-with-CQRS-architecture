using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Queries.GetAllRegions
{
    public class GetAllRegionsQuery : IRequest<DatatableViewModel>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllRegionsQuery, DatatableViewModel>
        {
            private readonly IRegionRepository regionRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IRegionRepository regionRepository, IMapper mapper)
            {
                this.regionRepository = regionRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
            {
                var regions = await regionRepository.GetAllAsync();
                IEnumerable<GetAllRegionsViewModel> regionsViewModel = _mapper.Map<IEnumerable<GetAllRegionsViewModel>>(regions);
                return new DatatableViewModel { data = regionsViewModel.OrderByDescending(x => x.Id) };
            }

        }
    }
}