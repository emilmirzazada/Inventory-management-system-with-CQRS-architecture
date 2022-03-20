using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.CreditCalls.Commands.CreateCreditCall
{
    public partial class CreateCreditCallCommand : IRequest<Response<int>>
    {
        public string Comment { get; set; }
        public int CreditId { get; set; }
        public string NextCallDate { get; set; }
        public int StatusId { get; set; }
        public string EmployeeId { get; set; }
    }
    public class CreateCreditCallCommandHandler : IRequestHandler<CreateCreditCallCommand, Response<int>>
    {
        private readonly ICreditCallRepository creditCallRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly IMapper _mapper;
        public CreateCreditCallCommandHandler(ICreditCallRepository creditCallRepository,
            IDateTimeService dateTimeService,
            IMapper mapper)
        {
            this.creditCallRepository = creditCallRepository;
            this.dateTimeService = dateTimeService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCreditCallCommand request, CancellationToken cancellationToken)
        {
            var creditCall = _mapper.Map<CreditCall>(request);
            creditCall.CallDate = dateTimeService.NowUtc;
            if (request.NextCallDate != null)
                creditCall.NextCallDate = DateTime.Parse(request.NextCallDate,CultureInfo.InvariantCulture);
            await creditCallRepository.AddAsync(creditCall);
            return new Response<int>(creditCall.Id);
        }
    }
}
