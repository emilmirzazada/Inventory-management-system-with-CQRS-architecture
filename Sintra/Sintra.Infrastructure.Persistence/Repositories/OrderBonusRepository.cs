using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Sintra.Infrastructure.Persistence.Repositories
{
    public class OrderBonusRepository : GenericRepositoryAsync<OrderBonus>, IOrderBonusRepository
    {
        private readonly DbSet<OrderBonus> orderBonuses;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly IDateTimeService dateTimeService;

        public OrderBonusRepository(IdentityContext dbContext, IMapper mapper,
            IDapper dapper, IDateTimeService dateTimeService) : base(dbContext, mapper)
        {
            orderBonuses = dbContext.Set<OrderBonus>();
            context = dbContext;
            this.dapper = dapper;
            this.dateTimeService = dateTimeService;
        }

        public IEnumerable<ApplicationUser> GetAllSellers()
        {
            var sellers = dapper.GetAll<ApplicationUser>
                    (@"select distinct u.FirstName, u.LastName,u.Id, sum(Price) as Balance,o.IsPaid
						from Orders o
                        left join users u on u.Id=o.EmployeeId
						group by u.FirstName, u.LastName,u.Id,o.IsPaid,o.CreditId
						having o.IsPaid=0 and o.CreditId is null", null, CommandType.Text);
            return sellers;
        }
        public IEnumerable<ApplicationUser> GetAllCreditCollectors()
        {
            var creditCollectors = dapper.GetAll<ApplicationUser>
                    (@"select distinct u.FirstName, u.LastName,u.Id, sum(amount) as Balance,t.IsPaid
						from Transactions t
                        left join users u on u.Id=t.EmployeeId
						group by u.FirstName, u.LastName,u.Id,t.IsPaid
						having t.IsPaid=0", null, CommandType.Text);

            return creditCollectors;
        }
        public void PayBonus(string employeeId, string payerId, decimal bonus)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", employeeId);
            dapper.Execute
                    (@"update OrderBonuses set BonusPaid=1 where
                       EmployeeId=@Id", p, CommandType.Text);

            var transaction = new EmployeeBonusTransaction
            {
                EmployeeId = employeeId,
                PayerEmployeeId = payerId,
                Amount = bonus,
                PayDate=dateTimeService.NowUtc
            };
            context.EmployeeBonusTransactions.Add(transaction);
            context.SaveChanges();
        }

        public IEnumerable<OrderBonus> GetAllOrderBonuses()
        {
            var orderBonuses = dapper.GetAll<OrderBonus>
                    (@"select ob.EmployeeId, sum(Bonus) as Bonus,BonusPaid
                        from orderbonuses ob
                        group by ob.EmployeeId,BonusPaid
                        having BonusPaid<>1", null, CommandType.Text);
            foreach (var orderbonus in orderBonuses)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Id", orderbonus.EmployeeId);
                orderbonus.Employee = dapper.Get<ApplicationUser>
                    (@"select * from Users
                        where Id=@Id", p, CommandType.Text);
            }
            return orderBonuses;
        }

    }
}
