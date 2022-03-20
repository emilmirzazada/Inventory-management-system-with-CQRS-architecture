using AutoMapper;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Repository;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : GenericRepositoryAsync<Service>, IServiceRepository
    {
        public ServiceRepository(IdentityContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}