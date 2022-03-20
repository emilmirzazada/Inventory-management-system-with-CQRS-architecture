using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Credits.Queries.GetCreditsByDate
{
    public class GetCreditsByDateQuery : IRequest<DatatableViewModel>
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public class GetCreditsByDateQueryHandler : IRequestHandler<GetCreditsByDateQuery, DatatableViewModel>
        {
            private readonly ICreditRepository _creditRepository;
            private readonly IMapper _mapper;
            public GetCreditsByDateQueryHandler(ICreditRepository creditRepository, IMapper mapper)
            {
                _creditRepository = creditRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetCreditsByDateQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<GetAllCreditsViewModel> credits = await _creditRepository.GetCredits(request.fromDate,request.toDate);
                return new DatatableViewModel { data = credits.OrderByDescending(x => x.Id) };
            }

        }
    }
}
