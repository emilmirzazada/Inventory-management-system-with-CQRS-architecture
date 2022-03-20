using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Regions.Commands.CreateRegion;
using Sintra.Application.Features.Regions.Commands.DeleteRegion;
using Sintra.Application.Features.Regions.Commands.UpdateRegion;
using Sintra.Application.Features.Regions.Commands.UpdateRegionUsers;
using Sintra.Application.Features.Regions.Queries.GetAllRegions;
using Sintra.Application.Features.Regions.Queries.GetRegionById;
using Sintra.Application.Features.Regions.Queries.GetRegionUsers;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class RegionController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Regionlar")]
        public IActionResult Regions()
        {
            return View();
        }

        [MyAuth]
        [HttpGet(Name = "Regionlar")]
        public async Task<JsonResult> GetRegions()
        {
            return Json(await Mediator.Send(new GetAllRegionsQuery()));
        }

        [MyAuth]
        [HttpPost(Name = "Regionların redaktəsi")]
        public async Task<JsonResult> CreateRegion(CreateRegionCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpGet(Name = "Regionlar")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            return Ok(await Mediator.Send(new GetRegionByIdQuery { Id = id }));
        }
        [MyAuth]
        [HttpPost(Name = "Regionların redaktəsi")]
        public async Task<JsonResult> UpdateRegion(UpdateRegionCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Regionların redaktəsi")]
        public async Task<IActionResult> DeleteRegionById(int id)
        {
            return Ok(await Mediator.Send(new DeleteRegionByIdCommand { Id = id }));
        }

        [MyAuth]
        [HttpGet(Name = "Regionların redaktəsi")]
        public async Task<IActionResult> ManageRegionUsers(int id)
        {
            return View(await Mediator.Send(new GetRegionUsersQuery { Id = id }));
        }

        [MyAuth]
        [HttpPost(Name = "Regionların redaktəsi")]
        public async Task<IActionResult> ManageRegionUsers(int regionId,
                        string regionName, string[] IdsToAdd, string[] IdsToDelete)
        {
            await Mediator.Send(new UpdateRegionUsersCommand
            {
                RegionId = regionId,
                RegionName=regionName,
                IdsToAdd=IdsToAdd,
                IdsToDelete=IdsToDelete
            });
            return RedirectToAction("Regions", "Region");
        }

        /*[HttpPost]
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
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
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
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return RedirectToAction("Roles", "Role");
        }*/
    }
}
