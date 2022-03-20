using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<DatatableViewModel>
    {
        public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, DatatableViewModel>
        {
            private readonly IGenericRepositoryAsync<Client> genericRepositoryAsync;
            private readonly IMapper _mapper;
            public GetAllClientsQueryHandler(IGenericRepositoryAsync<Client> genericRepositoryAsync, IMapper mapper)
            {
                this.genericRepositoryAsync = genericRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var clients = await genericRepositoryAsync.GetAsync<Client>();
                IEnumerable<GetAllClientsViewModel> clientsViewModel = 
                    _mapper.Map<IEnumerable<GetAllClientsViewModel>>(clients).OrderByDescending(x=>x.Id);
                return new DatatableViewModel { data= clientsViewModel };
            }

        }
    }
}
