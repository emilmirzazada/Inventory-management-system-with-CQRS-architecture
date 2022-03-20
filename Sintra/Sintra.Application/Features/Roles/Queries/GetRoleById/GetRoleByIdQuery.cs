using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Exceptions;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<Response<IdentityRole<string>>>
    {
        public string Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<IdentityRole<string>>>
        {
            private readonly RoleManager<IdentityRole<string>> roleManager;
            public GetProductByIdQueryHandler(RoleManager<IdentityRole<string>> roleManager)
            {
                this.roleManager = roleManager;
            }

            public async Task<Response<IdentityRole<string>>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
            {
                var role = await roleManager.Roles.FirstOrDefaultAsync(x=>x.Id==query.Id);
                if (role == null) throw new Exception($"Role Not Found.");
                return new Response<IdentityRole<string>>(role);
            }
        }
    }
}
