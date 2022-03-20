using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface ICreditRepository : IGenericRepositoryAsync<Credit>
    {
        Task<IEnumerable<GetAllCreditsViewModel>> GetProblematicCredits(string fromDate, string toDate);
        Task<IEnumerable<GetAllCreditsViewModel>> GetCredits(string fromDate, string toDate);
        Credit GetCreditById(int id);
    }
}
