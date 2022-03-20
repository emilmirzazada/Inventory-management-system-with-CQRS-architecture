using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Orders.Queries.GetOrderAccessories
{
    public class GetOrderAccessoriesQuery : IRequest<DatatableViewModel>
    {
        public int orderId { get; set; }
        public class GetOrderAccessoriesQueryHandler : IRequestHandler<GetOrderAccessoriesQuery, DatatableViewModel>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IMapper mapper;

            public GetOrderAccessoriesQueryHandler(IOrderRepository orderRepository,IMapper mapper)
            {
                this.orderRepository = orderRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetOrderAccessoriesQuery request, CancellationToken cancellationToken)
            {
                var orderAccessories = await orderRepository.GetOrderAccessories(request.orderId);
                IEnumerable<GetOrderAccessoriesViewModel> orderAccessoriesViewModels = mapper.Map<IEnumerable<GetOrderAccessoriesViewModel>>(orderAccessories);
                return new DatatableViewModel { data = orderAccessoriesViewModels };
            }

        }
    }
}
