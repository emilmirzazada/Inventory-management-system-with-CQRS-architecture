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

namespace Sintra.Application.Features.Sellers.Commands.RecieveBalance
{
    public partial class RecieveBalanceCommand : IRequest<Response<int>>
    {
        public string EmployeeId { get; set; }
        public string RecieverId { get; set; }
        public decimal Amount { get; set; }
        public class RecieveBalanceCommandHandler : IRequestHandler<RecieveBalanceCommand, Response<int>>
        {
            private readonly IUserRepository userRepository;
            public RecieveBalanceCommandHandler(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            public Task<Response<int>> Handle(RecieveBalanceCommand request, CancellationToken cancellationToken)
            {
                userRepository.RecieveBalance(request.EmployeeId, request.RecieverId, request.Amount);
                return Task.FromResult(new Response<int>());
            }
        }
    }
    
}
