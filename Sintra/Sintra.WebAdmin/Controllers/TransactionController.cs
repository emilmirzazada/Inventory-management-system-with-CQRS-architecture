using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Credits.Queries.GetCreditById;
using Sintra.Application.Features.Transactions.Commands.CreateTransaction;
using Sintra.Application.Features.Transactions.Queries.GetTransactionsByCreditId;
using Sintra.WebAdmin.StartupInjections.Authorization;

namespace Sintra.WebAdmin.Controllers
{
    public class TransactionController : BaseController
    {
        [MyAuth]
        [HttpGet(Name ="Tranzaksiyalar")]
        public async Task<IActionResult> CreditTransactions(int creditId)
        {
            ViewBag.Id=creditId;
            return View((await Mediator.Send(new GetCreditByIdQuery { Id = creditId })).Data);
        }

        [MyAuth]
        [HttpGet(Name = "Tranzaksiyalar")]
        public async Task<JsonResult> GetTransactionsByCreditId(int creditId)
        {
            return Json(await Mediator.Send(new GetTransactionsByCreditIdQuery{creditId=creditId}));
        }

        [MyAuth]
        [HttpPost(Name = "Tranzaksiyanın yaradılması")]
        public async Task<JsonResult> CreateTransaction(CreateTransactionCommand command)
        {
            command.EmployeeId=HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        // [HttpGet]
        // public async Task<IActionResult> GetTransactionById(int id)
        // {
        //     return Ok(await Mediator.Send(new GetTransactionByIdQuery { Id = id }));
        // }
        // [HttpPost]
        // /*[Authorize(Policy = "EditTransactions")]*/
        // public async Task<JsonResult> UpdateTransaction(UpdateTransactionCommand command)
        // {
        //     return Json(await Mediator.Send(command));
        // }
        // [HttpDelete]
        // public async Task<IActionResult> DeleteTransactionById(int id)
        // {
        //     return Ok(await Mediator.Send(new DeleteTransactionByIdCommand { Id = id }));
        // }
    }
}