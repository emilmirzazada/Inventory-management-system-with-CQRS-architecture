using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetWarehouseById
{
    public class GetWarehouseByIdQuery : IRequest<Response<Warehouse>>
    {
        public int Id { get; set; }
        public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, Response<Warehouse>>
        {
            private readonly IWarehouseRepository _WarehouseRepository;
            public GetWarehouseByIdQueryHandler(IWarehouseRepository WarehouseRepository)
            {
                _WarehouseRepository = WarehouseRepository;
            }
            public async Task<Response<Warehouse>> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
            {
                var Warehouse = await _WarehouseRepository.GetByIdAsync(query.Id);
                if (Warehouse == null) throw new Exception($"Warehouse Not Found.");
                return new Response<Warehouse>(Warehouse);
            }
        }
    }
}
