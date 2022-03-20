using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.CreditCollectors.Queries.GetAllCreditCollectors;
using Sintra.Application.Features.OrderBonuses.Commands.PayBonus;
using Sintra.Application.Features.OrderBonuses.Queries.GetAllOrderBonuses;
using Sintra.Application.Features.OrderBonuses.Queries.GetBonusTransactions;
using Sintra.Application.Features.Sellers.Queries.GetAllSellers;
using Sintra.Application.Features.Sellers.Queries.GetBalanceTransactions;
using Sintra.Application.Features.Sellers.Queries.GetCreditBalanceTransactions;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class FinanceController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Satıcılar")]
        public IActionResult Sellers()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Satıcılar")]
        public async Task<JsonResult> GetSellers()
        {
            return Json(await Mediator.Send(new GetAllSellersQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Satıcılar")]
        public IActionResult CreditCollectors()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Satıcılar")]
        public async Task<JsonResult> GetCreditCollectors()
        {
            return Json(await Mediator.Send(new GetAllCreditCollectorsQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Bonuslar")]
        public IActionResult OrderBonuses()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Bonuslar")]
        public async Task<JsonResult> GetOrderBonuses()
        {
            return Json(await Mediator.Send(new GetAllOrderBonusesQuery()));
        }
        [MyAuth]
        [HttpPost(Name = "Bonusun ödənməsi")]
        public async Task<JsonResult> PayBonus(PayBonusCommand command)
        {
            command.PayerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpGet(Name = "Bonus hesabatı")]
        public IActionResult BonusTransactions()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Bonus hesabatı")]
        public async Task<JsonResult> GetBonusTransactions()
        {
            return Json(await Mediator.Send(new GetBonusTransactionsQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Balans hesabatı")]
        public IActionResult BalanceTransactions()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Balans hesabatı")]
        public async Task<JsonResult> GetBalanceTransactions()
        {
            return Json(await Mediator.Send(new GetBalanceTransactionsQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Balans hesabatı")]
        public IActionResult CreditBalanceTransactions()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Balans hesabatı")]
        public async Task<JsonResult> GetCreditBalanceTransactions()
        {
            return Json(await Mediator.Send(new GetCreditBalanceTransactionsQuery()));
        }
    }
}
