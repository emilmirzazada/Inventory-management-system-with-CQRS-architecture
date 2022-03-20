using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Notiifications.Queries.GetUserNotifications;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NotificationController : BaseApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(Mediator.Send(new GetUserNotificationsQuery { UserId = User.GetUserId() }).Result);
        }
    }
}
