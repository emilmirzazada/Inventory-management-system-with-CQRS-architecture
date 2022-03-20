using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Commands.UpdateRegion
{
    public class UpdateRegionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, Response<int>>
        {
            private readonly IRegionRepository regionRepository;
            public UpdateRegionCommandHandler(IRegionRepository regionRepository)
            {
                this.regionRepository = regionRepository;
            }
            public async Task<Response<int>> Handle(UpdateRegionCommand command, CancellationToken cancellationToken)
            {
                var region = await regionRepository.GetByIdAsync(command.Id);

                if (region == null)
                {
                    throw new Exception($"Region Not Found.");
                }
                else
                {
                    region.Name = command.Name;
                    await regionRepository.UpdateAsync(region);
                    return new Response<int>(region.Id);
                }
            }
        }
    }
}
