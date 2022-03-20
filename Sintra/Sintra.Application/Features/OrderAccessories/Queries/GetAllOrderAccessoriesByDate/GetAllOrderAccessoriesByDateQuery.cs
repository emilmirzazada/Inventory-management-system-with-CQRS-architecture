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

namespace Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate
{
    public class GetAllOrderAccessoriesByDateQuery : IRequest<DatatableViewModel>
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public class GetAllOrderAccessoriesByDateQueryHandler : IRequestHandler<GetAllOrderAccessoriesByDateQuery, DatatableViewModel>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IMapper mapper;

            public GetAllOrderAccessoriesByDateQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                this.orderRepository = orderRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllOrderAccessoriesByDateQuery request, CancellationToken cancellationToken)
            {
                var orderAccessories = await orderRepository.GetAllOrderAccessories(request.fromDate,request.toDate);
                return new DatatableViewModel { data = orderAccessories.OrderByDescending(x => x.Id) };
            }

        }
    }
}
