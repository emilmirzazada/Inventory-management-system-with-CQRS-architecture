using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Roles.Commands.CreateRole
{
    public partial class CreateRoleCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly RoleManager<IdentityRole<string>> roleManager;
        private readonly IMapper _mapper;
        public CreateRoleCommandHandler(RoleManager<IdentityRole<string>> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<IdentityRole<string>>(request);
            await roleManager.CreateAsync(role);
            return new Response<string>(role.Id);
        }

    }
}
