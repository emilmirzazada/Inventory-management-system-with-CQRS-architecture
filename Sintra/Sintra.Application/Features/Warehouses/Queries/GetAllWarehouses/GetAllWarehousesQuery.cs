using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses
{
    public class GetAllWarehousesQuery : IRequest<DatatableViewModel>
    {
        public string userId { get; set; }
        public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, DatatableViewModel>
        {
            private readonly IWarehouseRepository _WarehouseRepository;
            private readonly IMapper _mapper;
            public GetAllWarehousesQueryHandler(IWarehouseRepository WarehouseRepository, IMapper mapper)
            {
                _WarehouseRepository = WarehouseRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
            {

                var Warehouses = _WarehouseRepository.Get<Warehouse, GetAllWarehousesViewModel>(x => x.Deleted == false);
                /*IEnumerable<GetAllWarehousesViewModel> WarehousesViewModel = _mapper.Map<IEnumerable<GetAllWarehousesViewModel>>(Warehouses);*/
                return new DatatableViewModel { data = Warehouses };
            }

        }
    }
}
