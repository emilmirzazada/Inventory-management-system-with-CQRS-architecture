using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseProductProduct
{
    public class UpdateWarehouseProductCommand : IRequest<Response<string>>
    {
        public int warehouseId { get; set; }
        public int productId { get; set; }
        public int balance { get; set; }

        public class UpdateWarehouseProductCommandHandler : IRequestHandler<UpdateWarehouseProductCommand, Response<string>>
        {
            private readonly IWarehouseProductRepository _WarehouseProductRepository;
            public UpdateWarehouseProductCommandHandler(IWarehouseProductRepository WarehouseProductRepository)
            {
                _WarehouseProductRepository = WarehouseProductRepository;
            }
            public async Task<Response<string>> Handle(UpdateWarehouseProductCommand command, CancellationToken cancellationToken)
            {
                var result = _WarehouseProductRepository.UpdateWarehouseProduct
                    (
                    command.warehouseId,command.productId,command.balance
                    );

                return new Response<string>(result);
            }
        }
    }
}