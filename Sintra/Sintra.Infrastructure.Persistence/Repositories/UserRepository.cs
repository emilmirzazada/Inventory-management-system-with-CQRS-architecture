using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : GenericRepositoryAsync<ApplicationUser>, IUserRepository
    {
        private readonly DbSet<ApplicationUser> _users;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext context;
        private readonly IDateTimeService dateTimeService;

        public UserRepository(IdentityContext dbContext, UserManager<ApplicationUser> userManager,
            IdentityContext context,IDateTimeService dateTimeService)
            : base(dbContext)
        {
            _users = dbContext.Set<ApplicationUser>();
            _userManager = userManager;
            this.context = context;
            this.dateTimeService = dateTimeService;
        }

        public virtual async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public void RecieveBalance(string employeeId,string recieverId,decimal amount)
        {
            ApplicationUser employee = context.Users.Where(x => x.Id == employeeId)?.FirstOrDefault();
            if (employee.Balance >= amount)
            {
                employee.Balance -= amount;
                context.Users.Update(employee);

                IEnumerable<Order> orders = context.Orders.Where(x => x.EmployeeId == employeeId);
                foreach (var item in orders)
                {
                    item.IsPaid = true;
                }
                context.Orders.UpdateRange(orders);
                context.SaveChanges();

                EmployeeBalanceTransaction employeeBalanceTransaction = new EmployeeBalanceTransaction
                {
                    EmployeeId = employeeId,
                    RecieverEmployeeId = recieverId,
                    Amount = amount,
                    RecieveDate = dateTimeService.NowUtc
                };
                context.EmployeeBalanceTransactions.Add(employeeBalanceTransaction);
                context.SaveChanges();
            }
            
        }

        public void RecieveCreditBalance(string employeeId, string recieverId, decimal amount)
        {
            ApplicationUser employee = context.Users.Where(x => x.Id == employeeId)?.FirstOrDefault();

            if (employee.Balance >= amount)
            {
                employee.Balance -= amount;
                context.Users.Update(employee);

                IEnumerable<Transaction> transactions = context.Transactions.Where(x => x.EmployeeId == employeeId);
                foreach (var item in transactions)
                {
                    item.IsPaid = true;
                }
                context.Transactions.UpdateRange(transactions);
                context.SaveChanges();

                EmployeeCreditBalanceTransaction employeeBalanceTransaction = new EmployeeCreditBalanceTransaction
                {
                    EmployeeId = employeeId,
                    RecieverEmployeeId = recieverId,
                    Amount = amount,
                    RecieveDate = dateTimeService.NowUtc
                };
                context.EmployeeCreditBalanceTransactions.Add(employeeBalanceTransaction);
                context.SaveChanges();
            }
            
        }
    }
}
