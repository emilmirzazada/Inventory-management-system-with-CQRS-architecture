using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Exceptions;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<string>>
        {
            private readonly RoleManager<IdentityRole<string>> roleManager;
            public UpdateRoleCommandHandler(RoleManager<IdentityRole<string>> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<Response<string>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
            {
                var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == command.Id);

                if (role == null)
                {
                    throw new Exception($"Role Not Found.");
                }
                else
                {
                    role.Name = command.Name;
                    await roleManager.UpdateAsync(role);
                    return new Response<string>(role.Id);
                }
            }
        }
    }
}
