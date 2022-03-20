using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.DeleteWarehouseProduct
{
    public class DeleteWarehouseProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public class DeleteWarehouseProductCommandHandler : IRequestHandler<DeleteWarehouseProductCommand, Response<int>>
        {
            private readonly IWarehouseProductRepository warehouseProductRepository;
            public DeleteWarehouseProductCommandHandler(IWarehouseProductRepository warehouseProductRepository)
            {
                this.warehouseProductRepository = warehouseProductRepository;
            }
            public async Task<Response<int>> Handle(DeleteWarehouseProductCommand command, CancellationToken cancellationToken)
            {
                warehouseProductRepository.DeleteWarehouseProduct(command.Id,command.productId);
                return new Response<int>(1);
            }
        }
    }
}