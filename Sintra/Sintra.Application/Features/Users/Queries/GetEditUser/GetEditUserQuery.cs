using MediatR;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.Features.Users.Commands.EditUser;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Users.Queries.GetEditUser
{
    public class GetEditUserQuery : IRequest<EditUserViewModel>
    {
        public string userId { get; set; }
        public class GetEditUserQueryHandler : IRequestHandler<GetEditUserQuery, EditUserViewModel>
        {
            private readonly UserManager<ApplicationUser> userManager;
            private readonly RoleManager<IdentityRole<string>> roleManager;

            public GetEditUserQueryHandler(UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole<string>> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }
            public async Task<EditUserViewModel> Handle(GetEditUserQuery query, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByIdAsync(query.userId);
                if (user != null)
                {
                    var selectedRoles = await userManager.GetRolesAsync(user);
                    var roles = roleManager.Roles.Where(x=>x.Name!="Superadmin").Select(i => i.Name);

                    var model =new EditUserViewModel()
                    {
                        UserId = user.Id,
                        isBlocked = user.IsBlocked,
                        SelectedRoles = selectedRoles,
                        AllRoles=roles,
                        Percentage=user.Percentage,
                        MinAmount=user.MinAmount,
                        MaxAmount=user.MaxAmount
                    };
                    return model;
                }
                throw new Exception($"User Not Found.");
                
            }
        }
    }
}
