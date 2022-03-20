using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IWarehouseProductRepository : IGenericRepositoryAsync<WarehouseProduct>
    {
        Task<List<WarehouseProduct>> GetWarehousesByProductId(int productId);
        Task<List<WarehouseProduct>> GetAllWarehouseProductsByWarehouseId(int id);
        void DeleteWarehouseProduct(int id, int productId);
        string UpdateWarehouseProduct(int warehouseId, int productId, int balance);
        WarehouseProduct GetWarehouseProduct(int id,int productId);
        string AddWarehouseProduct(int warehouseId, int productId,int balance);
    }
}
