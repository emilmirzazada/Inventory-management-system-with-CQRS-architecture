using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.CreditCollectors.Commands.RecieveCreditBalance
{
    public partial class RecieveCreditBalanceCommand : IRequest<Response<int>>
    {
        public string EmployeeId { get; set; }
        public string RecieverId { get; set; }
        public decimal Amount { get; set; }
        public class RecieveCreditBalanceCommandHandler : IRequestHandler<RecieveCreditBalanceCommand, Response<int>>
        {
            private readonly IUserRepository userRepository;
            public RecieveCreditBalanceCommandHandler(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            public Task<Response<int>> Handle(RecieveCreditBalanceCommand request, CancellationToken cancellationToken)
            {
                userRepository.RecieveCreditBalance(request.EmployeeId, request.RecieverId, request.Amount);
                return Task.FromResult(new Response<int>());
            }
        }
    }
    
}
