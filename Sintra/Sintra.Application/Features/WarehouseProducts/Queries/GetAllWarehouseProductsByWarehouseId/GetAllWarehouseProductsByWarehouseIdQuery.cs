using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetAllWarehouseProducts
{
    public class GetAllWarehouseProductsByWarehouseIdQuery : IRequest<DatatableViewModel>
    {
        public int Id { get; set; }
        public class GetAllWarehouseProductsByWarehouseIdQueryHandler : IRequestHandler<GetAllWarehouseProductsByWarehouseIdQuery, DatatableViewModel>
        {
            private readonly IWarehouseProductRepository warehouseProductRepository;
            private readonly IMapper _mapper;
            public GetAllWarehouseProductsByWarehouseIdQueryHandler(IWarehouseProductRepository warehouseProductRepository,
                IMapper mapper)
            {
                this.warehouseProductRepository = warehouseProductRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllWarehouseProductsByWarehouseIdQuery request, CancellationToken cancellationToken)
            {
                var WarehouseProducts = await warehouseProductRepository.GetAllWarehouseProductsByWarehouseId(request.Id);
                IEnumerable<GetAllWarehouseProductsViewModel> WarehousesViewModel = _mapper.Map<IEnumerable<GetAllWarehouseProductsViewModel>>(WarehouseProducts);
                return new DatatableViewModel { data = WarehousesViewModel };
            }

        }
    }
}
