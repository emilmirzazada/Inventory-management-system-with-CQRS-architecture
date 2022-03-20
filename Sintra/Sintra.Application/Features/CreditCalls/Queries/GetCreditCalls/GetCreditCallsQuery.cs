using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.CreditCalls.Queries.GetCreditCalls
{
    public class GetCreditCallsQuery : IRequest<DatatableViewModel>
    {
        public class GetCreditCallsQueryHandler : IRequestHandler<GetCreditCallsQuery, DatatableViewModel>
        {
            private readonly ICreditCallRepository creditCallRepository;
            private readonly IMapper _mapper;
            public GetCreditCallsQueryHandler(ICreditCallRepository creditCallRepository, IMapper mapper)
            {
                this.creditCallRepository = creditCallRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetCreditCallsQuery request, CancellationToken cancellationToken)
            {
                var creditCalls = await creditCallRepository.GetAsync<CreditCall, GetCreditCallsViewModel>
                    (includeProperties:"Employee,Credit");
                foreach (var item in creditCalls)
                {
                    item.Order= (await creditCallRepository.GetAsync<Order>
                    (x => x.CreditId == item.CreditId, includeProperties: "Client")
                    ).FirstOrDefault();
                }
                return new DatatableViewModel { data = creditCalls.OrderByDescending(x => x.Id) };
            }

        }
    }
}
