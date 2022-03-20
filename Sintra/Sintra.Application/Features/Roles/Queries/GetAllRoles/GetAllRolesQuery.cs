using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Roles.RoleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<DatatableViewModel>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, DatatableViewModel>
        {
            private readonly RoleManager<IdentityRole<string>> roleManager;
            private readonly IMapper _mapper;
            public GetAllRolesQueryHandler(RoleManager<IdentityRole<string>> roleManager, IMapper mapper)
            {
                this.roleManager = roleManager;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                var roles = roleManager.Roles.Where(x=>x.Name!="Superadmin").ToList();
                IEnumerable<GetAllRolesViewModel> rolesViewModel = _mapper.Map<IEnumerable<GetAllRolesViewModel>>(roles);
                return new DatatableViewModel { data = rolesViewModel.OrderByDescending(x => x.Id) };
            }

        }
    }
}
