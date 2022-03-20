using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.CreditCollectors.Queries.GetAllSellers;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.FeaturesMobile.CreditCollectors.Queries.GetAllCreditCollectors
{
    public class GetAllCreditCollectorsQuery : IRequest<DatatableViewModel>
    {
        public class GetAllCreditCollectorsQueryHandler : IRequestHandler<GetAllCreditCollectorsQuery, DatatableViewModel>
        {
            private readonly IOrderBonusRepository orderBonusRepository;
            private readonly IMapper mapper;

            public GetAllCreditCollectorsQueryHandler(IOrderBonusRepository orderBonusRepository,IMapper mapper)
            {
                this.orderBonusRepository = orderBonusRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllCreditCollectorsQuery request, CancellationToken cancellationToken)
            {
                var CreditCollectors = orderBonusRepository.GetAllCreditCollectors();
                return new DatatableViewModel { data = CreditCollectors.OrderByDescending(x => x.Id) };
            }

        }
    }
}
