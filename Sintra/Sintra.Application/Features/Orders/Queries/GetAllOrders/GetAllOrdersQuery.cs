using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<DatatableViewModel>
    {
        public int ClientId { get; set; }
        public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, DatatableViewModel>
        {
            private readonly IOrderRepository _OrderRepository;
            public GetAllOrdersQueryHandler(IOrderRepository OrderRepository)
            {
                _OrderRepository = OrderRepository;
            }

            public async Task<DatatableViewModel> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<GetAllOrdersViewModel> orders;
                if (request.ClientId==0)
                {
                    orders = _OrderRepository.Get<Order, GetAllOrdersViewModel>
                                (x => x.Deleted == false, includeProperties: "Product,Employee,Client");
                }
                else
                {
                    orders = _OrderRepository.Get<Order, GetAllOrdersViewModel>
                                (x => x.Deleted == false && x.ClientId == request.ClientId
                                , includeProperties: "Product,Employee,Client");
                }
                return new DatatableViewModel { data = orders.OrderByDescending(x => x.Id) };
            }

        }
    }
}
