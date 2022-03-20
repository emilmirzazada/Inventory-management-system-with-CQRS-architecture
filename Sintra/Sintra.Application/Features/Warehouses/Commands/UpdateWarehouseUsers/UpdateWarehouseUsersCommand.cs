using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseUsers
{
    public class UpdateWarehouseUsersCommand : IRequest<Response<int>>
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
        public class UpdateWarehouseUsersCommandHandler : IRequestHandler<UpdateWarehouseUsersCommand, Response<int>>
        {
            private readonly IWarehouseRepository WarehouseRepository;
            public UpdateWarehouseUsersCommandHandler(IWarehouseRepository WarehouseRepository)
            {
                this.WarehouseRepository = WarehouseRepository;
            }
            public async Task<Response<int>> Handle(UpdateWarehouseUsersCommand command, CancellationToken cancellationToken)
            {
                WarehouseRepository.EditWarehouseUsers(command);
                return new Response<int>(1);
            }
        }
    }
}
