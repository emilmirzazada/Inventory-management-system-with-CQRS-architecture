using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Commands.CreateRegion
{
    public partial class CreateRegionCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
    }
    public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, Response<int>>
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper _mapper;
        public CreateRegionCommandHandler(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var region = _mapper.Map<Region>(request);
            await regionRepository.AddAsync(region);
            return new Response<int>(region.Id);
        }
    }
}
