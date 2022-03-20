using Sintra.Application.Features.Transactions.Commands.CreateTransaction;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepositoryAsync<Transaction>
    {
        Task<string> CreateTransaction(CreateTransactionCommand command);
    }
}
