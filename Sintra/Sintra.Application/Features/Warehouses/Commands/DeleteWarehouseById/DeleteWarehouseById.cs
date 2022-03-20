using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.DeleteWarehouseById
{
    public class DeleteWarehouseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Response<int>>
        {
            private readonly IWarehouseRepository warehouseRepository;
            public DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
            {
                this.warehouseRepository = warehouseRepository;
            }
            public async Task<Response<int>> Handle(DeleteWarehouseCommand command, CancellationToken cancellationToken)
            {
                var Warehouse = await warehouseRepository.GetByIdAsync(command.Id);

                if (Warehouse == null)
                {
                    throw new Exception($"Warehouse Not Found.");
                }
                else
                {
                    Warehouse.Deleted = true;
                    await warehouseRepository.MakeDeleted(Warehouse);
                    return new Response<int>(Warehouse.Id);
                }
            }
        }
    }
}
