using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<DatatableViewModel>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, DatatableViewModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }


            public async Task<DatatableViewModel> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = (await _userRepository.GetAllAsync()).Where(x=>x.Email!="sintraadmin@gmail.com");
                IEnumerable<GetAllUsersViewModel> usersViewModel = _mapper.Map<IEnumerable<GetAllUsersViewModel>>(users);
                return new DatatableViewModel { data = usersViewModel.OrderByDescending(x => x.Id) };
            }
        }
    }


}
