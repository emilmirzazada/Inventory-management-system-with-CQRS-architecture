using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate;
using Sintra.Application.Features.OrderAccessories.Queries.GetDelayedOrderAccessories;
using Sintra.Application.Features.Orders.Commands.CreateOrder;
using Sintra.Application.Features.Orders.Commands.DeleteOrder;
using Sintra.Application.Features.Orders.Queries.GetAllOrders;
using Sintra.Application.Features.Orders.Queries.GetOrderAccessories;
using Sintra.Application.Features.Products.Queries.GetAllProducts;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class OrderController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Sifarişlər")]
        public async Task<IActionResult> Orders(int clientId)
        {
            ViewBag.clientId = clientId;
            return View(((IEnumerable<GetAllProductsViewModel>)(await Mediator.Send(new GetAllProductsQuery())).data));
        }

        [MyAuth]
        [HttpGet(Name = "Sifarişlər")]
        public async Task<JsonResult> GetOrders(int clientId)
        {
            return Json(await Mediator.Send(new GetAllOrdersQuery { ClientId=clientId}));
        }
        [MyAuth]
        [HttpPost(Name = "Sifarişin redaktəsi")]
        public async Task<JsonResult> CreateOrder(CreateOrderCommand command)
        {
            command.EmployeeId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpDelete(Name = "Sifarişin redaktəsi")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await Mediator.Send(new DeleteOrderCommand { Id = id }));
        }
        [MyAuth]
        [HttpGet(Name = "Sifarişlər")]
        public IActionResult OrderAccessories(int orderId)
        {
            ViewBag.Id = orderId;
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Sifarişlər")]
        public async Task<JsonResult> GetOrderAccessories(int orderId)
        {
            return Json(await Mediator.Send(new GetOrderAccessoriesQuery { orderId = orderId }));
        }
        public async Task<IActionResult> AllOrderAccessories(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Sifariş aksessuarları")]
        public async Task<JsonResult> GetAllOrderAccessories()
        {
            return Json(await Mediator.Send(new GetAllOrderAccessoriesByDateQuery ()));
        }
        [MyAuth]
        [HttpGet(Name = "Sifariş aksessuarları")]
        public IActionResult DelayedOrderAccessories()
        {
            return View();
        }
        [MyAuth]
        [HttpGet(Name = "Sifariş aksessuarları")]
        public async Task<JsonResult> GetDelayedOrderAccessories()
        {
            return Json(await Mediator.Send(new GetDelayedOrderAccessoriesQuery()));
        }
        [MyAuth]
        [HttpPost(Name = "Aksessuarların yenilənməsi")]
        public async Task<JsonResult> UpdateOrderAccessory(UpdateOrderAccessoryCommand command)
        {
            return Json(await Mediator.Send(command));
        }

    }
}
