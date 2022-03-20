using MediatR;
using Sintra.Application.Exceptions;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<ApplicationUser>>
    {
        public string Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<ApplicationUser>>
        {
            private readonly IUserRepository _userRepository;
            public GetUserByIdQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<Response<ApplicationUser>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByIdAsync(query.Id);
                if (user == null) throw new ApiException($"User Not Found.");
                return new Response<ApplicationUser>(user);
            }
        }
    }
}
