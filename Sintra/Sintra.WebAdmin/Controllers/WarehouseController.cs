using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.Products.Queries.GetAllProducts;
using Sintra.Application.Features.WarehouseProducts.Queries.GetWarehousesByProductId;
using Sintra.Application.Features.Warehouses.Commands.AddWarehouseProduct;
using Sintra.Application.Features.Warehouses.Commands.CreateWarehouse;
using Sintra.Application.Features.Warehouses.Commands.DeleteWarehouseById;
using Sintra.Application.Features.Warehouses.Commands.DeleteWarehouseProduct;
using Sintra.Application.Features.Warehouses.Commands.UpdateWarehouse;
using Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseProductProduct;
using Sintra.Application.Features.Warehouses.Commands.UpdateWarehouseUsers;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouseProducts;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseById;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseProduct;
using Sintra.Application.Features.Warehouses.Queries.GetWarehouseUsers;
using Sintra.Domain.Entities;
using Sintra.WebAdmin.Models;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class WarehouseController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Anbarlar")]
        public IActionResult Warehouses()
        {
            return View();
        }

        [MyAuth]
        [HttpGet(Name = "Anbarlar")]
        public async Task<JsonResult> GetWarehouses()
        {
            return Json(await Mediator.Send(new GetWarehousesByUserIdQuery
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            }));
        }

        [MyAuth]
        [HttpGet(Name = "Anbarlar")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            return Ok(await Mediator.Send(new GetWarehouseByIdQuery { Id = id }));
        }

        [MyAuth]
        [HttpPost(Name = "Anbarların redaktəsi")]
        public async Task<JsonResult> UpdateWarehouse(UpdateWarehouseCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpPost(Name = "Anbarların redaktəsi")]
        public async Task<JsonResult> CreateWarehouse(CreateWarehouseCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Anbarların redaktəsi")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            return Ok(await Mediator.Send(new DeleteWarehouseCommand { Id = id }));
        }
        [MyAuth]
        [HttpGet(Name = "Anbar məhsulları")]
        public async Task<IActionResult> ManageWarehouseProducts(int id)
        {
            ViewBag.Id = id;
            return View(new ManageWarehouseProductsViewModel
            {
                Products = ((IEnumerable<GetAllProductsViewModel>)(await Mediator.Send(new GetAllProductsQuery())).data),
                Warehouses = ((IEnumerable<GetAllWarehousesViewModel>)(await Mediator.Send(new GetAllWarehousesQuery())).data).Where(x => x.Id != id)
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetWarehouseProductsByWarehouseId(int id)
        {
            return Json(await Mediator.Send(new GetAllWarehouseProductsByWarehouseIdQuery { Id = id }));
        }
        [HttpGet]
        public async Task<JsonResult> GetWarehouseProduct(int id, int productId)
        {
            return Json(await Mediator.Send(new GetWarehouseProductQuery { Id = id, productId = productId }));
        }
        [MyAuth]
        [HttpPost(Name = "Anbar məhsullarının redaktəsi")]
        public async Task<JsonResult> AddWarehouseProduct(AddWarehouseProductCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Anbar məhsullarının redaktəsi")]
        public async Task<JsonResult> UpdateWarehouseProduct(UpdateWarehouseProductCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpDelete(Name = "Anbar məhsullarının redaktəsi")]
        public async Task<IActionResult> DeleteWarehouseProduct(int id, int productId)
        {
            return Ok(await Mediator.Send(new DeleteWarehouseProductCommand { Id = id, productId = productId }));
        }

        [MyAuth]
        [HttpGet(Name = "Anbar işçiləri")]
        public async Task<IActionResult> ManageWarehouseUsers(int id)
        {
            return View(await Mediator.Send(new GetWarehouseUsersQuery { Id = id }));
        }

        [MyAuth]
        [HttpPost(Name = "Anbar işçilərinin redaktəsi")]
        public async Task<IActionResult> ManageWarehouseUsers(int warehouseId,
                        string warehouseName, string[] IdsToAdd, string[] IdsToDelete)
        {
            await Mediator.Send(new UpdateWarehouseUsersCommand
            {
                WarehouseId = warehouseId,
                WarehouseName = warehouseName,
                IdsToAdd = IdsToAdd,
                IdsToDelete = IdsToDelete
            });
            return RedirectToAction("Warehouses", "Warehouse");
        }

        [HttpGet]
        public async Task<JsonResult> GetWarehousesByProductId(int productId)
        {
            return Json(await Mediator.Send(new GetWarehousesByProductIdQuery { productId = productId }));
        }
    }
}