using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Regions.Commands.UpdateRegionUsers
{
    public class UpdateRegionUsersCommand : IRequest<Response<int>>
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
        public class UpdateRegionUsersCommandHandler : IRequestHandler<UpdateRegionUsersCommand, Response<int>>
        {
            private readonly IRegionRepository regionRepository;
            public UpdateRegionUsersCommandHandler(IRegionRepository regionRepository)
            {
                this.regionRepository = regionRepository;
            }
            public async Task<Response<int>> Handle(UpdateRegionUsersCommand command, CancellationToken cancellationToken)
            {
                regionRepository.EditRegionUsers(command);
                return new Response<int>(1);
            }
        }
    }
}
