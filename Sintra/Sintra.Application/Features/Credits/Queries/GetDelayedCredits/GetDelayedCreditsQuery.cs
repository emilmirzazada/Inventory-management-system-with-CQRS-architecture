using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Credits.Queries.GetDelayedCredits
{
    public class GetDelayedCreditsQuery : IRequest<DatatableViewModel>
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public class GetDelayedCreditsQueryHandler : IRequestHandler<GetDelayedCreditsQuery, DatatableViewModel>
        {
            private readonly ICreditRepository _creditRepository;
            private readonly IDateTimeService dateTimeService;
            private readonly IMapper _mapper;
            public GetDelayedCreditsQueryHandler(ICreditRepository creditRepository,IDateTimeService dateTimeService,
                IMapper mapper)
            {
                _creditRepository = creditRepository;
                this.dateTimeService = dateTimeService;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetDelayedCreditsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<GetAllCreditsViewModel> credits =
                    await _creditRepository.GetCredits(null,dateTimeService.NowUtc.ToString());
                return new DatatableViewModel { data = credits.OrderByDescending(x => x.Id) };
            }

        }
    }
}
