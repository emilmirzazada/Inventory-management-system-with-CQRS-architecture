using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteRegionByIdCommandHandler : IRequestHandler<DeleteRegionByIdCommand, Response<int>>
        {
            private readonly IRegionRepository regionRepository;
            public DeleteRegionByIdCommandHandler(IRegionRepository regionRepository)
            {
                this.regionRepository = regionRepository;
            }
            public async Task<Response<int>> Handle(DeleteRegionByIdCommand command, CancellationToken cancellationToken)
            {
                var Region = await regionRepository.GetByIdAsync(command.Id);
                if (Region == null) throw new Exception($"Region Not Found.");
                await regionRepository.DeleteAsync(Region);
                return new Response<int>(Region.Id);
            }
        }
    }
}
