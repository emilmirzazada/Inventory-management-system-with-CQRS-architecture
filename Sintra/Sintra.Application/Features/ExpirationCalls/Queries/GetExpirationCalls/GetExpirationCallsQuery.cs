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

namespace Sintra.Application.Features.ExpirationCalls.Queries.GetExpirationCalls
{
    public class GetExpirationCallsQuery : IRequest<DatatableViewModel>
    {
        public class GetExpirationCallsQueryHandler : IRequestHandler<GetExpirationCallsQuery, DatatableViewModel>
        {
            private readonly IExpirationCallRepository expirationCallRepository;
            private readonly IMapper _mapper;
            public GetExpirationCallsQueryHandler(IExpirationCallRepository expirationCallRepository, IMapper mapper)
            {
                this.expirationCallRepository = expirationCallRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetExpirationCallsQuery request, CancellationToken cancellationToken)
            {
                var ExpirationCalls = await expirationCallRepository.GetAsync<ExpirationCall, GetExpirationCallsViewModel>
                    (includeProperties: "Employee,OrderAccessory.Accessory,OrderAccessory.Order.Client");
                
                return new DatatableViewModel { data = ExpirationCalls.OrderByDescending(x => x.Id) };
            }

        }
    }
}
