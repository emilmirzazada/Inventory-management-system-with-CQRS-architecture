using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IOrderBonusRepository : IGenericRepositoryAsync<OrderBonus>
    {
        IEnumerable<ApplicationUser> GetAllCreditCollectors();
        void PayBonus(string employeeId, string payerId, decimal bonus);
        IEnumerable<ApplicationUser> GetAllSellers();
        IEnumerable<OrderBonus> GetAllOrderBonuses();
    }
}
