using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.WarehouseProducts.Queries.GetWarehousesByProductId;
using Sintra.Application.FeaturesMobile.Warehouses.Queries.GetAllWarehouses;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WarehouseController : BaseApiController
    {
        [MyApiAuth]
        [HttpGet(Name = "Anbarlar")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetWarehousesByUserIdQuery
            {
                userId = User.GetUserId()
            }));
        }
        [HttpGet("GetAllWarehouses")]
        [Authorize]
        public async Task<IActionResult> GetAllWarehouses()
        {
            return Ok(await Mediator.Send(new GetAllWarehousesQuery()));
        }
        [HttpGet("GetWarehousesByProductId")]
        [Authorize]
        public async Task<IActionResult> GetWarehousesByProductId(int id)
        {
            return Ok(await Mediator.Send(new GetWarehousesByProductIdQuery {productId=id }));
        }
    }
}
