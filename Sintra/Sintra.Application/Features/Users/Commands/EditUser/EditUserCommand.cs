using MediatR;
using Microsoft.AspNetCore.Identity;
using Sintra.Application.Exceptions;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public string userId { get; set; }
        public bool isBlocked { get; set; }
        public decimal Percentage { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public string[] SelectedRoles { get; set; }
        public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Response<string>>
        {
            private readonly UserManager<ApplicationUser> userManager;
            public EditUserCommandHandler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<Response<string>> Handle(EditUserCommand command, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByIdAsync(command.userId);
                if (user != null)
                {
                    user.IsBlocked = command.isBlocked;
                    user.Percentage = command.Percentage;
                    user.MinAmount = command.MinAmount;
                    user.MaxAmount = command.MaxAmount;
                    await userManager.UpdateAsync(user);

                    var userRoles = await userManager.GetRolesAsync(user);
                    command.SelectedRoles = command.SelectedRoles ?? new string[] { };
                    await userManager.AddToRolesAsync(user, command.SelectedRoles.Except(userRoles).ToArray<string>());
                    await userManager.RemoveFromRolesAsync(user, userRoles.Except(command.SelectedRoles).ToArray<string>());
                    return new Response<string>(user.Id);
                }
                throw new Exception($"User Not Found.");
            }
        }
    }
}
