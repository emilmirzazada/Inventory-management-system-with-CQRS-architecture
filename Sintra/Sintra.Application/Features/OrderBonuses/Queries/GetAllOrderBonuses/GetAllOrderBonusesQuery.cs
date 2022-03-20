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

namespace Sintra.Application.Features.OrderBonuses.Queries.GetAllOrderBonuses
{
    public class GetAllOrderBonusesQuery : IRequest<DatatableViewModel>
    {
        public class GetAllOrderBonusesQueryHandler : IRequestHandler<GetAllOrderBonusesQuery, DatatableViewModel>
        {
            private readonly IOrderBonusRepository orderBonusRepository;
            private readonly IMapper mapper;

            public GetAllOrderBonusesQueryHandler(IOrderBonusRepository orderBonusRepository, IMapper mapper)
            {
                this.orderBonusRepository = orderBonusRepository;
                this.mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllOrderBonusesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var OrderBonuses = orderBonusRepository.GetAllOrderBonuses();
                    var OrderBonusesViewModels = mapper.Map<IEnumerable<GetAllOrderBonusesViewModel>>(OrderBonuses);
                    return new DatatableViewModel { data = OrderBonusesViewModels.OrderByDescending(x => x.Id) };
                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }

        }
    }
}
