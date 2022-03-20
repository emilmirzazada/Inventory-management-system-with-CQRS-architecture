using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class ExpirationCallRepository : GenericRepositoryAsync<ExpirationCall>, IExpirationCallRepository
    {
        private readonly DbSet<ExpirationCall> creditCalls;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IDateTimeService dateTimeService;

        public ExpirationCallRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            creditCalls = dbContext.Set<ExpirationCall>();
            context = dbContext;
            this.dapper = dapper;
            this.dateTimeService = dateTimeService;
        }


    }
}
