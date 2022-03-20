using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Clients.Queries.GetAllClients;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class ClientController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Müştərilər")]
        public IActionResult Clients()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Müştərilər")]
        public async Task<JsonResult> GetClients()
        {
            return Json(await Mediator.Send(new GetAllClientsQuery()));
        }
    }
}
