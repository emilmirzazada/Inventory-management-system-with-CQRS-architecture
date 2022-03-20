using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses
{
    public class GetWarehousesByUserIdQuery : IRequest<DatatableViewModel>
    {
        public string userId { get; set; }
        public class GetAllWarehousesQueryHandler : IRequestHandler<GetWarehousesByUserIdQuery, DatatableViewModel>
        {
            private readonly IWarehouseRepository _WarehouseRepository;
            private readonly IMapper _mapper;
            public GetAllWarehousesQueryHandler(IWarehouseRepository WarehouseRepository, IMapper mapper)
            {
                _WarehouseRepository = WarehouseRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetWarehousesByUserIdQuery request, CancellationToken cancellationToken)
            {

                var Warehouses = _WarehouseRepository.GetWarehouses(request.userId);
                /*IEnumerable<GetAllWarehousesViewModel> WarehousesViewModel = _mapper.Map<IEnumerable<GetAllWarehousesViewModel>>(Warehouses);*/
                return new DatatableViewModel { data = Warehouses };
            }
            
        }
    }
}
