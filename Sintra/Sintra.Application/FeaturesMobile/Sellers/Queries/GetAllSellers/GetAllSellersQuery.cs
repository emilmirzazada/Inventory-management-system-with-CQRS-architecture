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

namespace Sintra.Application.FeaturesMobile.Sellers.Queries.GetAllSellers
{
    public class GetAllSellersQuery : IRequest<DatatableViewModel>
    {
        public class GetAllSellersQueryHandler : IRequestHandler<GetAllSellersQuery, DatatableViewModel>
        {
            private readonly IOrderBonusRepository orderBonusRepository;
            private readonly IMapper mapper;

            public GetAllSellersQueryHandler(IOrderBonusRepository orderBonusRepository,IMapper mapper)
            {
                this.orderBonusRepository = orderBonusRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllSellersQuery request, CancellationToken cancellationToken)
            {
                var Sellers = orderBonusRepository.GetAllSellers();
                return new DatatableViewModel { data = Sellers.OrderByDescending(x => x.Id) };
            }

        }
    }
}
