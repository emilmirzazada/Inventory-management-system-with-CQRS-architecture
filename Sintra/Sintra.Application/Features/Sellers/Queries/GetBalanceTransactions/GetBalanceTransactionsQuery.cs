using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces;
using Sintra.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Sellers.Queries.GetBalanceTransactions
{
    public class GetBalanceTransactionsQuery : IRequest<DatatableViewModel>
    {
        public class GetBalanceTransactionsQueryHandler : IRequestHandler<GetBalanceTransactionsQuery, DatatableViewModel>
        {
            private readonly IGenericRepositoryAsync<EmployeeBalanceTransaction> genericRepository;
            public GetBalanceTransactionsQueryHandler(IGenericRepositoryAsync<EmployeeBalanceTransaction> genericRepository)
            {
                this.genericRepository = genericRepository;
            }

            public async Task<DatatableViewModel> Handle(GetBalanceTransactionsQuery request, CancellationToken cancellationToken)
            {
                var balanceTransactions = genericRepository.
                    Get<EmployeeBalanceTransaction, GetBalanceTransactionsViewModel>(
                    includeProperties:"Employee,RecieverEmployee");
                return new DatatableViewModel { data = balanceTransactions.OrderByDescending(x => x.Id) };
            }

        }
    }
}
