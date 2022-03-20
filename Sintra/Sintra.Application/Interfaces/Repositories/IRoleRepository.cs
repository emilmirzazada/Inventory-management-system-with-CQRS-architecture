using Microsoft.AspNetCore.Identity;
using Sintra.Application.DTOs.Claims;
using Sintra.Application.Features.Roles.RoleModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        void ManageRoleClaims(RoleClaimsViewModel model);
    }
}
