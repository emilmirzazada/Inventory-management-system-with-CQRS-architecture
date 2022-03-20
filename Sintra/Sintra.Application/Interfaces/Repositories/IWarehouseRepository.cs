using Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseUsers;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseUsers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IWarehouseRepository : IGenericRepositoryAsync<Warehouse>
    {
        List<GetAllWarehousesViewModel> GetWarehouses(string userId);
        List<Warehouse> GetMobileWarehouses(string userId);
        void EditWarehouseUsers(UpdateWarehouseUsersCommand model);
        Task<WarehouseUsersModel> GetWarehouseUsers(int regionId);
    }
}
