using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces;
using Sintra.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.OrderBonuses.Queries.GetBonusTransactions
{
    public class GetBonusTransactionsQuery : IRequest<DatatableViewModel>
    {
        public class GetBonusTransactionsQueryHandler : IRequestHandler<GetBonusTransactionsQuery, DatatableViewModel>
        {
            private readonly IGenericRepositoryAsync<EmployeeBonusTransaction> genericRepository;
            public GetBonusTransactionsQueryHandler(IGenericRepositoryAsync<EmployeeBonusTransaction> genericRepository)
            {
                this.genericRepository = genericRepository;
            }

            public async Task<DatatableViewModel> Handle(GetBonusTransactionsQuery request, CancellationToken cancellationToken)
            {
                var BonusTransactions = genericRepository.Get<EmployeeBonusTransaction, GetBonusTransactionsViewModel>(
                    includeProperties: "Employee,PayerEmployee");
                return new DatatableViewModel { data = BonusTransactions.OrderByDescending(x => x.Id) };
            }

        }
    }
}
