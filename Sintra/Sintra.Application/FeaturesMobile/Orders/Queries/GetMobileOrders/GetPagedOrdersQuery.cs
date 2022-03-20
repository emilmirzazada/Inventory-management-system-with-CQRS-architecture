using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.FeaturesMobile.Orders.Queries.GetMobileOrders
{
    public class GetPagedOrdersQuery : IRequest<DatatableViewModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ClientId { get; set; }
        public class GetPagedOrdersQueryHandler : IRequestHandler<GetPagedOrdersQuery, DatatableViewModel>
        {
            private readonly IOrderRepository _OrderRepository;
            public GetPagedOrdersQueryHandler(IOrderRepository OrderRepository)
            {
                _OrderRepository = OrderRepository;
            }

            public async Task<DatatableViewModel> Handle(GetPagedOrdersQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Order> orders;
                if (request.ClientId==0)
                {
                    orders = _OrderRepository.Get<Order, GetMobileOrdersViewModel>
                                (x => x.Deleted == false, includeProperties: "Product,Employee,Client")
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .OrderByDescending(x => x.Id)
                                .Take(request.PageSize);
                }
                else
                {
                    orders = _OrderRepository.Get<Order, GetMobileOrdersViewModel>
                                (x => x.Deleted == false && x.ClientId==request.ClientId
                                , includeProperties: "Product,Employee,Client")
                                .OrderByDescending(x => x.Id)
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize);
                }
                return new DatatableViewModel { data = orders };
            }

        }
    }
}
