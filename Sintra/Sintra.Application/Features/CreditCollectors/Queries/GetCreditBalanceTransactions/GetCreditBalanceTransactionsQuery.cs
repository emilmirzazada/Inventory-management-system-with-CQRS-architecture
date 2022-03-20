using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Sellers.Queries.GetBalanceTransactions;
using Sintra.Application.Interfaces;
using Sintra.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Sellers.Queries.GetCreditBalanceTransactions
{
    public class GetCreditBalanceTransactionsQuery : IRequest<DatatableViewModel>
    {
        public class GetCreditBalanceTransactionsQueryHandler : IRequestHandler<GetCreditBalanceTransactionsQuery, DatatableViewModel>
        {
            private readonly IGenericRepositoryAsync<EmployeeBalanceTransaction> genericRepository;
            public GetCreditBalanceTransactionsQueryHandler(IGenericRepositoryAsync<EmployeeBalanceTransaction> genericRepository)
            {
                this.genericRepository = genericRepository;
            }

            public async Task<DatatableViewModel> Handle(GetCreditBalanceTransactionsQuery request, CancellationToken cancellationToken)
            {
                var balanceTransactions = genericRepository.
                    Get<EmployeeCreditBalanceTransaction, GetCreditBalanceTransactionsViewModel>(
                    includeProperties:"Employee,RecieverEmployee");
                return new DatatableViewModel { data = balanceTransactions.OrderByDescending(x => x.Id) };
            }

        }
    }
}
