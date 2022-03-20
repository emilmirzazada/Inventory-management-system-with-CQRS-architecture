using AutoMapper;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Repository;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class RecipeRepository : GenericRepositoryAsync<Receipe>, IReceipeRepository
    {
        public RecipeRepository(IdentityContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}