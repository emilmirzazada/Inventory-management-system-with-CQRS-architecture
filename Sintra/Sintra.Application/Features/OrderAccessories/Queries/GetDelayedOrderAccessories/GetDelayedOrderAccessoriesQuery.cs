using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.OrderAccessories.Queries.GetDelayedOrderAccessories
{
    public class GetDelayedOrderAccessoriesQuery : IRequest<DatatableViewModel>
    {
        public class GetAllOrderAccessoriesByDateQueryHandler : IRequestHandler<GetDelayedOrderAccessoriesQuery, DatatableViewModel>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IMapper mapper;

            public GetAllOrderAccessoriesByDateQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                this.orderRepository = orderRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetDelayedOrderAccessoriesQuery request, CancellationToken cancellationToken)
            {
                var orderAccessories = await orderRepository.GetDelayedOrderAccessories();
                return new DatatableViewModel { data = orderAccessories.OrderByDescending(x => x.Id) };
            }

        }
    }
}
