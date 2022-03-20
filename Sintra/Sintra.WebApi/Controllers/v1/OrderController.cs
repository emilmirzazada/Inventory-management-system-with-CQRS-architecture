using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Orders.Commands.CreateOrder;
using Sintra.Application.Features.Orders.Queries.GetAllOrders;
using Sintra.Application.FeaturesMobile.Orders.Queries.GetMobileOrders;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Sifarişlər")]
        [Authorize]
        public async Task<IActionResult> Get(int pageSize,int pageNumber,int clientId)
        {
            return Ok(await Mediator.Send(new GetPagedOrdersQuery
            {
                ClientId = clientId,
                PageSize=pageSize,
                PageNumber=pageNumber
            }));
        }

        [MyApiAuth]
        [HttpPost("CreateOrder",Name = "Sifarişin redaktəsi")]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            command.EmployeeId = User.GetUserId();
            return Ok(await Mediator.Send(command));
        }
    }
}
