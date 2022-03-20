using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.ProductTransfers.Commands.ApproveProductTransfer;
using Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer;
using Sintra.Application.Features.ProductTransfers.Commands.RejectProductTransfer;
using Sintra.Application.Features.ProductTransfers.Queries.GetProductTransferById;
using Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts;
using Sintra.Application.Features.ProductTransferTransfers.Queries.GetAllProductTransferTransfers;
using Sintra.Domain.Entities;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class ProductTransferController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Transferlər")]
        public IActionResult ProductTransfers()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Transferlər")]
        public async Task<JsonResult> GetProductTransfers()
        {
            return Json(await Mediator.Send(new GetAllProductTransfersQuery()));
        }
        [MyAuth]
        [HttpPost(Name = "Transferin cavablanması")]
        public async Task<JsonResult> ApproveProductTransfer(ApproveProductTransferCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Transferin cavablanması")]
        public async Task<JsonResult> RejectProductTransfer(RejectProductTransferCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpGet(Name = "Transferlər")]
        public async Task<JsonResult> GetProductTransferById(int id)
        {
            return Json(await Mediator.Send(new GetProductTransferByIdQuery { Id = id }));
        }
        [MyAuth]
        [HttpPost(Name = "Transferin yaradılması")]
        public async Task<JsonResult> CreateProductTransfer(
            int fromWarehouseId, int toWarehouseId,
            List<ProductQuantityModel> products)
        {
            return Json(await Mediator.Send(new CreateProductTransferCommand
            {
                products = products,
                fromWarehouseId = fromWarehouseId,
                toWarehouseId = toWarehouseId
            }));
        }

        #region TransferProducts
        [MyAuth]
        [HttpGet(Name = "Transferlər")]
        public async Task<IActionResult> TransferProducts(int transferId)
        {
            return View(await Mediator.Send(new GetProductTransferByIdQuery
            {
                Id = transferId,
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            }));
        }
        [MyAuth]
        [HttpGet(Name = "Transferlər")]
        public async Task<JsonResult> GetTransferProducts(int transferId)
        {
            return Json(await Mediator.Send(new GetTransferProductsQuery
            {
                transferId = transferId,
                userId= HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            }));
        }
        #endregion
    }
}
