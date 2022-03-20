using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetWarehouseProduct
{
    public class GetWarehouseProductQuery : IRequest<Response<WarehouseProduct>>
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public class GetWarehouseProductQueryHandler : IRequestHandler<GetWarehouseProductQuery, Response<WarehouseProduct>>
        {
            private readonly IWarehouseProductRepository _WarehouseProductRepository;
            public GetWarehouseProductQueryHandler(IWarehouseProductRepository WarehouseProductRepository)
            {
                _WarehouseProductRepository = WarehouseProductRepository;
            }
            public async Task<Response<WarehouseProduct>> Handle(GetWarehouseProductQuery query, CancellationToken cancellationToken)
            {
                var warehouseProduct = _WarehouseProductRepository.GetWarehouseProduct(query.Id,query.productId);
                if (warehouseProduct == null) throw new Exception($"Warehouse Product Not Found.");
                return new Response<WarehouseProduct>(warehouseProduct);
            }
        }
    }
}
