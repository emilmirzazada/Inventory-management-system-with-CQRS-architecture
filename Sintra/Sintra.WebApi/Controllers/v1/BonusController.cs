using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.OrderBonuses.Commands.PayBonus;
using Sintra.Application.Features.OrderBonuses.Queries.GetAllOrderBonuses;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    public class BonusController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Bonuslar")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllOrderBonusesQuery()));
        }

        [MyApiAuth]
        [HttpPost(Name = "Bonusun ödənməsi")]
        [Authorize]
        public async Task<IActionResult> ReceiveBalance([FromBody] PayBonusCommand command)
        {
            command.PayerId = User.GetUserId();
            return Ok(await Mediator.Send(command));
        }
    }
}
