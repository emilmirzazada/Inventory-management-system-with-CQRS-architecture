using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Orders.Commands.CreateOrder
{
    public partial class CreateOrderCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int warehouseId { get; set; }
        public decimal Price { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public string FinCode { get; set; }
        public int Term { get; set; }
        public decimal InitialPayment { get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<string>>
    {
        private readonly IOrderRepository _OrderRepository;
        public CreateOrderCommandHandler(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public Task<Response<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = _OrderRepository.CreateOrder(request);
            return Task.FromResult(new Response<string>(result));
        }
    }
}
