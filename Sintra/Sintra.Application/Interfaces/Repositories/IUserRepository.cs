using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepositoryAsync<ApplicationUser>
    {
        void RecieveCreditBalance(string employeeId, string recieverId, decimal amount);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        void RecieveBalance(string employeeId, string recieverId, decimal amount);
    }
}
