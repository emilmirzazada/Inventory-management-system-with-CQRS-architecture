using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Sellers.Commands.RecieveBalance;
using Sintra.Application.FeaturesMobile.Sellers.Queries.GetAllSellers;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SellersController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Satıcılar")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllSellersQuery()));
        }

        [MyApiAuth]
        [HttpPost(Name = "Balansın qəbul edilməsi")]
        [Authorize]
        public async Task<IActionResult> ReceiveBalance([FromBody] RecieveBalanceCommand command)
        {
            command.RecieverId = User.GetUserId();
            return Ok(await Mediator.Send(command));
        }
    }
}
