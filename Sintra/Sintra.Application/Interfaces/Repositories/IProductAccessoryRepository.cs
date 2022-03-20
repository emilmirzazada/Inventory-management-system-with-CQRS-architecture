using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IProductAccessoryRepository : IGenericRepositoryAsync<ProductAccessory>
    {
        Task<List<ProductAccessory>> GetAllProductAccessoriesByProductId(int id);
        void DeleteProductAccessory(int id, int accessoryId);
        ProductAccessory GetProductAccessory(int id, int accessoryId);
        string AddProductAccessory(int parentId, int accessoryId);
    }
}
