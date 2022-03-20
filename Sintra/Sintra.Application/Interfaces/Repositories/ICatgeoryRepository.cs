using Sintra.Application.Features.Categories.Queries.GetAllCategories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepositoryAsync<Category>
    {

    }
}
