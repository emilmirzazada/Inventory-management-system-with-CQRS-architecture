using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Features.Transactions.Commands.CreateTransaction;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepositoryAsync<Transaction>, ITransactionRepository
    {
        private readonly DbSet<Transaction> Transactions;
        private readonly IdentityContext ctx;
        private readonly IDateTimeService dateTimeService;
        public readonly IMapper mapper;

        public TransactionRepository(IdentityContext dbContext, IDateTimeService dateTimeService,
           IMapper mapper) : base(dbContext, mapper)
        {
            Transactions = dbContext.Set<Transaction>();
            this.ctx = dbContext;
            this.dateTimeService = dateTimeService;
            this.mapper = mapper;
        }

        public async Task<string> CreateTransaction(CreateTransactionCommand command)
        {
            try
            {
                Transaction Transaction = mapper.Map<Transaction>(command);
                Transaction.Credit=ctx.Credits.Where(x=>x.Id==command.CreditId).FirstOrDefault();
                Transaction.Date = dateTimeService.NowUtc;
                Transaction.PreviousDebt=Transaction.Credit.Debt;
                Transaction.PresentDebt=Transaction.Credit.Debt-command.Amount;
                await AddAsync(Transaction);

                var user = ctx.Users.Where(x => x.Id == command.EmployeeId).FirstOrDefault();
                user.Balance += command.Amount;
                ctx.SaveChanges();

                Credit credit = ctx.Credits.Where(x=>x.Id==command.CreditId).FirstOrDefault();
                credit.Debt -= command.Amount;
                ctx.Credits.Update(credit);
                ctx.SaveChanges();

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
    }
}
