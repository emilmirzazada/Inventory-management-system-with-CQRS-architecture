using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Transactions.Commands.CreateTransaction
{
    public partial class CreateTransactionCommand : IRequest<Response<string>>
    {
        public int CreditId { get; set; }
        public string EmployeeId { get; set; }
        public decimal Amount { get; set; }
    }
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<string>>
    {
        private readonly ITransactionRepository _TransactionRepository;

        public CreateTransactionCommandHandler(ITransactionRepository TransactionRepository)
        {
            _TransactionRepository = TransactionRepository;
        }

        public async Task<Response<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var result = await _TransactionRepository.CreateTransaction(request);


            return new Response<string>(result);
        }
    }
}
