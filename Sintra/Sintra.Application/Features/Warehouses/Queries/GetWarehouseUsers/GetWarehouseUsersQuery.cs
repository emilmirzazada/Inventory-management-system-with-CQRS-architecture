using MediatR;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Queries.GetWarehouseUsers
{
    public class GetWarehouseUsersQuery : IRequest<WarehouseUsersModel>
    {
        public int Id { get; set; }
        public class GetWarehouseUsersQueryHandler : IRequestHandler<GetWarehouseUsersQuery, WarehouseUsersModel>
        {
            private readonly IWarehouseRepository _WarehouseRepository;
            public GetWarehouseUsersQueryHandler(IWarehouseRepository WarehouseRepository)
            {
                _WarehouseRepository = WarehouseRepository;
            }
            public async Task<WarehouseUsersModel> Handle(GetWarehouseUsersQuery query, CancellationToken cancellationToken)
            {
                var WarehouseUsers = await _WarehouseRepository.GetWarehouseUsers(query.Id);
                if (WarehouseUsers == null) throw new Exception($"Warehouse Not Found.");
                return WarehouseUsers;
            }
        }
    }
}
