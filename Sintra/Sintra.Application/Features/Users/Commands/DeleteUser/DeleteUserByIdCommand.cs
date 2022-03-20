using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Exceptions;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserByIdCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, Response<string>>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public DeleteUserByIdCommandHandler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<Response<string>> Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == command.Id);
                if (user == null) throw new Exception($"User Not Found.");
                await userManager.DeleteAsync(user);
                return new Response<string>(user.Id);
            }
        }
    }
}
