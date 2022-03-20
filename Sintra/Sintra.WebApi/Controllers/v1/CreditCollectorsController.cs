using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.CreditCollectors.Commands.RecieveCreditBalance;
using Sintra.Application.FeaturesMobile.CreditCollectors.Queries.GetAllCreditCollectors;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CreditCollectorsController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Satıcılar ")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllCreditCollectorsQuery()));
        }

        [MyApiAuth]
        [HttpPost(Name = "Balansın qəbul edilməsi ")]
        [Authorize]
        public async Task<IActionResult> ReceiveCreditBalance([FromBody]RecieveCreditBalanceCommand command)
        {
            command.RecieverId = User.GetUserId();
            return Ok(await Mediator.Send(command));
        }
    }
}
