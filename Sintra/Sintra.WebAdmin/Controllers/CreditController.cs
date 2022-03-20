using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Credits.Queries.GetCreditById;
using Sintra.Application.Features.Credits.Queries.GetCreditsByDate;
using Sintra.Application.Features.Credits.Queries.GetDelayedCredits;
using Sintra.Application.Features.Credits.Queries.GetProblematicCredits;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class CreditController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public async Task<IActionResult> GetCreditById(int id)
        {
            return Ok(await Mediator.Send(new GetCreditByIdQuery { Id = id }));
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public async Task<IActionResult> Credits(DateTime? fromDate,DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public async Task<IActionResult> GetCredits(string toDate,string fromDate)
        {
            return Ok(await Mediator.Send(new GetCreditsByDateQuery { fromDate = fromDate,toDate=toDate }));
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public IActionResult DelayedCredits()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public async Task<IActionResult> GetDelayedCredits()
        {
            return Ok(await Mediator.Send(new GetDelayedCreditsQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public IActionResult ProblematicCredits(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Kreditlər")]
        public async Task<IActionResult> GetProblematicCredits(string toDate, string fromDate)
        {
            return Ok(await Mediator.Send(new GetProblematicCreditsQuery()));
        }
    }
}
