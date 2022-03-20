using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<int>>
        {
            private readonly IOrderRepository orderRepository;
            public DeleteOrderCommandHandler(IOrderRepository orderRepository)
            {
                this.orderRepository = orderRepository;
            }
            public async Task<Response<int>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
            {
                /*var res = orderRepository.DeleteOrder(command.Id);*/

                var order = await orderRepository.GetByIdAsync(command.Id);

                if (order == null)
                {
                    throw new Exception($"order Not Found.");
                }
                else
                {
                    order.Deleted = true;
                    await orderRepository.MakeDeleted(order);
                    return new Response<int>(order.Id);
                }
            }
        }
    }
}
