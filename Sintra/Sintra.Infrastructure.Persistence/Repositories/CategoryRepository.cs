using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
    {
        private readonly DbSet<Category> _orders;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IMapper mapper;
        private readonly IDateTimeService dateTimeService;

        public CategoryRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            _orders = dbContext.Set<Category>();
            context = dbContext;
            this.dapper = dapper;
            this.mapper = mapper;
            this.dateTimeService = dateTimeService;
        }
    }
}
