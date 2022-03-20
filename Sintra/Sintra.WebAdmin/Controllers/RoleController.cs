using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Sintra.Application.DTOs.Claims;
using Sintra.Application.Features.Roles.Commands.CreateRole;
using Sintra.Application.Features.Roles.Commands.DeleteRole;
using Sintra.Application.Features.Roles.Commands.UpdateRole;
using Sintra.Application.Features.Roles.Queries.GetAllRoles;
using Sintra.Application.Features.Roles.Queries.GetRoleById;
using Sintra.Application.Features.Roles.RoleModels;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<string>> roleManager;
        private readonly IRoleRepository roleRepository;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<string>> roleManager,
            IRoleRepository roleRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.roleRepository = roleRepository;
        }
        [MyAuth]
        [HttpGet(Name = "Rollar")]
        public IActionResult Roles()
        {
            return View();
        }

        [MyAuth]
        [HttpGet(Name ="Rol icazələri")]
        public async Task<IActionResult> ManageRoleClaims(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={roleId} cannot be found";
                return NotFound();
            }
            var existingRoleCLaims = await roleManager.GetClaimsAsync(role);
            var model = new RoleClaimsViewModel
            {
                RoleId = roleId
            };

            var actions = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(Controller).IsAssignableFrom(type))
        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any()
        && m.GetCustomAttributes(typeof(MyAuthAttribute), true).Any())
        .Select(x => new { Controller = x.DeclaringType.Name, Name = x.GetCustomAttribute<HttpMethodAttribute>().Name })
        .OrderBy(x => x.Controller).ToList();

            foreach (var item in actions)
            {
                RoleClaim roleClaim = new RoleClaim
                {
                    ClaimType=item.Name,
                    ClaimValue = item.Controller
                };

                if (existingRoleCLaims.Any(c => c.Type == item.Name))
                {
                    roleClaim.IsSelected = true;
                }
                model.Claims.Add(roleClaim);
            }
            model.Claims = model.Claims.Distinct(new ClaimsComparer()).ToList();
            return View(model);
        }

        class ClaimsComparer : IEqualityComparer<RoleClaim>
        {
            public bool Equals( RoleClaim x, RoleClaim y)
            {
                return x.ClaimType == y.ClaimType;
            }

            public int GetHashCode([DisallowNull] RoleClaim obj)
            {
                return obj.ClaimType.GetHashCode();
            }
        }

        [MyAuth]
        [HttpPost(Name = "Rol icazələri")]
        public async Task<IActionResult> ManageRoleClaims(RoleClaimsViewModel model)
        {
            roleRepository.ManageRoleClaims(model);
            return RedirectToAction("Roles", "Role");
        }
       
        

        [MyAuth]
        [HttpPost(Name = "Rol istifadəçiləri")]
        public async Task<IActionResult> ManageRoleUsers(RoleEditModel model)
        {
            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Can't add to role");
                    }
                }
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Can't remove from role");
                    }
                }
            }
            return RedirectToAction("Roles", "Role");
        }

        [MyAuth]
        [HttpGet(Name = "Rol istifadəçiləri")]
        public async Task<IActionResult> ManageRoleUsers(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            var members = new List<ApplicationUser>();
            var nonmembers = new List<ApplicationUser>();
            foreach (var user in userManager.Users.Where(x => x.Email != "sintraadmin@gmail.com"))
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                                 ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetRoles()
        {
            return Json(await Mediator.Send(new GetAllRolesQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById(string id)
        {
            return Ok(await Mediator.Send(new GetRoleByIdQuery { Id = id }));
        }

        [MyAuth]
        [HttpPost(Name = "Rolların redaktəsi")]
        public async Task<JsonResult> CreateRole(CreateRoleCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpPost(Name = "Rolların redaktəsi")]
        public async Task<JsonResult> UpdateRole(UpdateRoleCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpGet(Name = "Rolların redaktəsi")]
        public async Task<IActionResult> DeleteRoleById(string id)
        {
            return Ok(await Mediator.Send(new DeleteRoleByIdCommand { Id = id }));
        }
    }
}
