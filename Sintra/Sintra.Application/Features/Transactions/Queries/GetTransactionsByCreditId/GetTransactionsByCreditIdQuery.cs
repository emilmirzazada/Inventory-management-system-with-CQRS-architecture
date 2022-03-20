using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Transactions.Queries.GetAllTransactions;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Transactions.Queries.GetTransactionsByCreditId
{
    public class GetTransactionsByCreditIdQuery : IRequest<DatatableViewModel>
    {
        public int creditId { get; set; } 
        public class GetTransactionsByCreditIdQueryHandler : IRequestHandler<GetTransactionsByCreditIdQuery, DatatableViewModel>
        {
            private readonly ITransactionRepository _TransactionRepository;
            private readonly IMapper _mapper;
            public GetTransactionsByCreditIdQueryHandler(ITransactionRepository TransactionRepository, IMapper mapper)
            {
                _TransactionRepository = TransactionRepository;
                _mapper = mapper;
            }

            public Task<DatatableViewModel> Handle(GetTransactionsByCreditIdQuery request, CancellationToken cancellationToken)
            {
                var Transactions = _TransactionRepository.Get<Transaction, GetAllTransactionsViewModel>
                                (x=>x.CreditId==request.creditId,includeProperties:"Credit,Employee");
                return Task.FromResult(new DatatableViewModel { data = Transactions.OrderByDescending(x => x.Id) });
            }
            
        }
    }
    
}
