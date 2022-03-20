using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.OrderBonuses.Commands.PayBonus
{
    public partial class PayBonusCommand : IRequest<Response<int>>
    {
        public string EmployeeId { get; set; }
        public string PayerId { get; set; }
        public decimal Amount { get; set; }
        public class PayBonusCommandHandler : IRequestHandler<PayBonusCommand, Response<int>>
        {
            private readonly IOrderBonusRepository orderBonusRepository;
            public PayBonusCommandHandler(IOrderBonusRepository orderBonusRepository)
            {
                this.orderBonusRepository = orderBonusRepository;
            }

            public Task<Response<int>> Handle(PayBonusCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    orderBonusRepository.PayBonus(request.EmployeeId, request.PayerId, request.Amount);
                    return Task.FromResult(new Response<int>());
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
