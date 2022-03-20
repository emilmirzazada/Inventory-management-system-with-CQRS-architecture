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
    public class CreditCallRepository : GenericRepositoryAsync<CreditCall>, ICreditCallRepository
    {
        private readonly DbSet<CreditCall> creditCalls;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IDateTimeService dateTimeService;

        public CreditCallRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            creditCalls = dbContext.Set<CreditCall>();
            context = dbContext;
            this.dapper = dapper;
            this.dateTimeService = dateTimeService;
        }


    }
}
