using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface ICategoryAccessoryRepository : IGenericRepositoryAsync<CategoryAccessory>
    {
        Task<List<CategoryAccessory>> GetCategoryAccessories(int id);
        void DeleteCategoryAccessory(int id, int accessoryId);
        string AddCategoryAccessory(int categoryId, int accessoryId);
    }
}
