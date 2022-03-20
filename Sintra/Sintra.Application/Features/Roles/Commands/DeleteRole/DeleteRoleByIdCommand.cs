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

namespace Sintra.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleByIdCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public class DeleteRoleByIdCommandHandler : IRequestHandler<DeleteRoleByIdCommand, Response<string>>
        {
            private readonly RoleManager<IdentityRole<string>> roleManager;

            public DeleteRoleByIdCommandHandler(RoleManager<IdentityRole<string>> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<Response<string>> Handle(DeleteRoleByIdCommand command, CancellationToken cancellationToken)
            {
                var Role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == command.Id);
                try
                {
                    if (Role == null) throw new Exception($"Role Not Found.");
                    await roleManager.DeleteAsync(Role);
                    return new Response<string>("ok");
                }
                catch (Exception ex)
                {
                    Response<string> response = new Response<string>();
                    response.Errors.Add(ex.Message);
                    return new Response<string>("fk");
                }

            }
        }
    }
}
