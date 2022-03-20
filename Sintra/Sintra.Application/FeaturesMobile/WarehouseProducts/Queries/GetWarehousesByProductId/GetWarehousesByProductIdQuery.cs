using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.FeaturesMobile.WarehouseProducts.Queries.GetWarehousesByProductId
{
    public class GetWarehousesByProductIdQuery : IRequest<List<WarehouseProduct>>
    {
        public int productId { get; set; }
        public class GetWarehousesByProductIdQueryHandler : IRequestHandler<GetWarehousesByProductIdQuery, List<WarehouseProduct>>
        {
            private readonly IWarehouseProductRepository warehouseProductRepository;
            public GetWarehousesByProductIdQueryHandler(IWarehouseProductRepository warehouseProductRepository)
            {
                this.warehouseProductRepository = warehouseProductRepository;
            }
            public async Task<List<WarehouseProduct>> Handle(GetWarehousesByProductIdQuery query, CancellationToken cancellationToken)
            {
                var warehouseProducts = await warehouseProductRepository.GetWarehousesByProductId(query.productId);
                return warehouseProducts;
            }
        }
    }
}
