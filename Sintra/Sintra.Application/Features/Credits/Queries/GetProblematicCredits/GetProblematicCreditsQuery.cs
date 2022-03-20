using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Credits.Queries.GetProblematicCredits
{
    public class GetProblematicCreditsQuery : IRequest<DatatableViewModel>
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public class GetProblematicCreditsQueryHandler : IRequestHandler<GetProblematicCreditsQuery, DatatableViewModel>
        {
            private readonly ICreditRepository _creditRepository;
            private readonly IMapper _mapper;
            public GetProblematicCreditsQueryHandler(ICreditRepository creditRepository, IMapper mapper)
            {
                _creditRepository = creditRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetProblematicCreditsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<GetAllCreditsViewModel> credits = await _creditRepository.GetProblematicCredits(request.fromDate, request.toDate);
                return new DatatableViewModel { data = credits.OrderByDescending(x => x.Id) };
            }

        }
    }
}
