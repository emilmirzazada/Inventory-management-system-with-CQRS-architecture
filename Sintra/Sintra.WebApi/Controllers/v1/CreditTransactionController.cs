using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Transactions.Commands.CreateTransaction;
using Sintra.Application.Features.Transactions.Queries.GetTransactionsByCreditId;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CreditTransactionController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Tranzaksiyalar")]
        [Authorize]
        public async Task<IActionResult> Get(int creditId)
        {
            return Ok(await Mediator.Send(new GetTransactionsByCreditIdQuery { creditId = creditId }));
        }
        [MyApiAuth]
        [HttpPost(Name = "Tranzaksiyanın yaradılması")]
        [Authorize]
        public async Task<IActionResult> CreateTransaction([FromBody]CreateTransactionCommand command)
        {
            command.EmployeeId = User.GetUserId();
            return Ok(await Mediator.Send(command));
        }
    }
}
