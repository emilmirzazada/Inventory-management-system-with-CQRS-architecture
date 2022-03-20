using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.CreditCalls.Commands.CreateCreditCall;
using Sintra.Application.Features.CreditCalls.Queries.GetCreditCalls;
using Sintra.Application.Features.ExpirationCalls.Commands.CreateExpirationCall;
using Sintra.Application.Features.ExpirationCalls.Queries.GetExpirationCalls;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class CallController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Zənglər")]
        public async Task<IActionResult> CreditCalls()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Zənglər")]
        public async Task<JsonResult> GetCreditCalls()
        {
            return Json(await Mediator.Send(new GetCreditCallsQuery()));
        }
        [MyAuth]
        [HttpPost(Name = "Zənglərin redaktəsi")]
        public async Task<JsonResult> CreateCreditCall(CreateCreditCallCommand command)
        {
            command.EmployeeId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Zənglərin redaktəsi")]
        public async Task<JsonResult> CreateExpirationCall(CreateExpirationCallCommand command)
        {
            command.EmployeeId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpGet(Name = "Zənglər")]
        public IActionResult ExpirationCalls()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Zənglər")]
        public async Task<JsonResult> GetExpirationCalls()
        {
            return Json(await Mediator.Send(new GetExpirationCallsQuery()));
        }
    }
}
