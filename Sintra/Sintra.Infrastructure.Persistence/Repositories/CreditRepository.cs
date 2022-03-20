using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class CreditRepository : GenericRepositoryAsync<Credit>, ICreditRepository
    {
        private readonly DbSet<Order> _orders;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IMapper mapper;
        private readonly IDateTimeService dateTimeService;

        public CreditRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            _orders = dbContext.Set<Order>();
            context = dbContext;
            this.dapper = dapper;
            this.mapper = mapper;
            this.dateTimeService = dateTimeService;
        }

        public Credit GetCreditById(int id)
        {
            Credit credit = context.Credits.Where(x => x.Id == id).FirstOrDefault();
            return credit;
        }

        public async Task<IEnumerable<GetAllCreditsViewModel>> GetCredits(string fromDate,string toDate)
        {
            IEnumerable<GetAllCreditsViewModel> credits;
            if (!(fromDate is { }) && toDate == null)
                credits = await GetAsync<Credit, GetAllCreditsViewModel>();
            else if(fromDate!=null && toDate==null)
                credits = await GetAsync<Credit, GetAllCreditsViewModel>
                    (x => x.Date >= DateTime.Parse(fromDate));
            else if (fromDate == null && toDate != null)
                credits = await GetAsync<Credit, GetAllCreditsViewModel>
                    (x => x.Date <= DateTime.Parse(toDate));
            else
                credits = await GetAsync<Credit, GetAllCreditsViewModel>
                    (x => x.Date >= DateTime.Parse(fromDate) && x.Date <= DateTime.Parse(toDate));
            foreach (var credit in credits)
            {
                credit.Order = (await GetAsync<Order>
                    (x => x.CreditId == credit.Id,includeProperties:"Client,Product,Employee")
                    ).FirstOrDefault();
            }

            return credits;
        }

        public async Task<IEnumerable<GetAllCreditsViewModel>> GetProblematicCredits(string fromDate, string toDate)
        {
            IEnumerable<GetAllCreditsViewModel> credits = context.CreditCalls
                                                                 .Include(c => c.Credit)
                                                                 .Where(c => c.StatusId == 4)
                                                                 .Select(c => mapper.Map<GetAllCreditsViewModel>(c.Credit))
                                                                 .Distinct()
                                                                 .ToList();
            foreach (var credit in credits)
            {
                credit.Order = (await GetAsync<Order>
                    (x => x.CreditId == credit.Id, includeProperties: "Client,Product,Employee")
                    ).FirstOrDefault();
            }
            return credits;
        }
    }
}
