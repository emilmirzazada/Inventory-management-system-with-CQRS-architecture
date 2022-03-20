using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.ProductTransfers.Commands.ApproveProductTransfer;
using Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer;
using Sintra.Application.Features.ProductTransfers.Commands.RejectProductTransfer;
using Sintra.Application.Features.ProductTransfers.Queries.GetProductTransferById;
using Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts;
using Sintra.WebApi.Extensions;
using Sintra.WebApi.PostBodyModels;
using Sintra.WebApi.StartupInjections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TransferController : BaseApiController
    {
        [MyApiAuth]
        [HttpPost("Create", Name = "Transferin yaradılması")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTransferData Data)
        {
            return Ok(await Mediator.Send(new CreateProductTransferCommand
            {
                products = Data.products,
                fromWarehouseId = Data.fromWarehouseId,
                toWarehouseId = Data.toWarehouseId
            }));
        }
        [MyApiAuth]
        [Authorize]
        [HttpPost("Approve",Name = "Transferin cavablanması")]
        public async Task<IActionResult> Approve([FromBody]ApproveProductTransferCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [MyApiAuth]
        [Authorize]
        [HttpPost("Reject",Name = "Transferin cavablanması ")]
        public async Task<IActionResult> Reject([FromBody]RejectProductTransferCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("GetProductTransferById")]
        public async Task<IActionResult> GetProductTransferById(int transferId)
        {
            return Ok(await Mediator.Send(new GetProductTransferByIdQuery
            {
                Id=transferId,
                userId=User.GetUserId()
            }));
        }
        [MyApiAuth]
        [HttpGet(Name = "Transferlər")]
        [Authorize]
        public async Task<IActionResult> GetTransferProducts(int transferId)
        {
            return Ok(await Mediator.Send(new GetTransferProductsQuery
            {
                transferId = transferId,
                userId=User.GetUserId()
            }));
        }
    }
}
