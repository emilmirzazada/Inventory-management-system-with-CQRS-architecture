using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Credits.Queries.GetCreditById
{
    public class GetCreditByIdQuery : IRequest<Response<Credit>>
    {
        public int Id { get; set; }
        public class GetCreditByIdQueryHandler : IRequestHandler<GetCreditByIdQuery, Response<Credit>>
        {
            private readonly ICreditRepository creditRepository;
            public GetCreditByIdQueryHandler(ICreditRepository creditRepository)
            {
                this.creditRepository = creditRepository;
            }
            public async Task<Response<Credit>> Handle(GetCreditByIdQuery query, CancellationToken cancellationToken)
            {
                var credit = creditRepository.GetCreditById(query.Id);
                return new Response<Credit>(credit);
            }
        }
    }
}
