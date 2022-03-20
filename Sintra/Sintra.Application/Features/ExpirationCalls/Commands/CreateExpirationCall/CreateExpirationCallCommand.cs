using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ExpirationCalls.Commands.CreateExpirationCall
{
    public partial class CreateExpirationCallCommand : IRequest<Response<int>>
    {
        public string Comment { get; set; }
        public int OrderAccessoryId { get; set; }
        public string NextCallDate { get; set; }
        public int StatusId { get; set; }
        public string EmployeeId { get; set; }
    }
    public class CreateExpirationCallCommandHandler : IRequestHandler<CreateExpirationCallCommand, Response<int>>
    {
        private readonly IExpirationCallRepository expirationCallRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly IMapper _mapper;
        public CreateExpirationCallCommandHandler(IExpirationCallRepository expirationCallRepository,
            IDateTimeService dateTimeService,
            IMapper mapper)
        {
            this.expirationCallRepository = expirationCallRepository;
            this.dateTimeService = dateTimeService;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateExpirationCallCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var expirationCall = _mapper.Map<ExpirationCall>(request);
                expirationCall.CallDate = dateTimeService.NowUtc;
                if (request.NextCallDate != null)
                    expirationCall.NextCallDate = DateTime.Parse(request.NextCallDate);
                await expirationCallRepository.AddAsync(expirationCall);
                return new Response<int>(expirationCall.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
