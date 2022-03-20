using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouseProducts;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WarehouseProductController : BaseApiController
    {
        [MyApiAuth]
        [Authorize]
        [HttpGet(Name = "Anbar məhsulları")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetAllWarehouseProductsByWarehouseIdQuery { Id=id}));
        }
    }
}
