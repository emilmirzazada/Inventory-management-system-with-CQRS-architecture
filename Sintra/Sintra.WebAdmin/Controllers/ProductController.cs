using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Categories.Queries.GetAllCategories;
using Sintra.Application.Features.ProductAccessories.Commands.AddProductAccessory;
using Sintra.Application.Features.ProductAccessories.Commands.DeleteProductAccessory;
using Sintra.Application.Features.ProductAccessories.Queries.GetAllProductAccessories;
using Sintra.Application.Features.Products.Commands.CreateProduct;
using Sintra.Application.Features.Products.Commands.DeleteProductById;
using Sintra.Application.Features.Products.Commands.UpdateProduct;
using Sintra.Application.Features.Products.Queries.GetAllAccessories;
using Sintra.Application.Features.Products.Queries.GetAllProducts;
using Sintra.Application.Features.Products.Queries.GetProductById;
using Sintra.Domain.Entities;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    
    public class ProductController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Məhsullar")]
        public async Task<IActionResult> Products()
        {
            return View((IEnumerable<Category>)(await Mediator.Send(new GetAllCategoriesQuery())).data);
        }

        [MyAuth]
        [HttpGet(Name = "Məhsullar")]
        public async Task<JsonResult> GetProducts()
        {
            return Json(await Mediator.Send(new GetAllProductsQuery()));
        }

        [MyAuth]
        [HttpGet(Name = "Məhsullar")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }
        [MyAuth]
        [HttpPost(Name = "Məhsulların redaktəsi")]
        public async Task<JsonResult> UpdateProduct(UpdateProductCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpPost(Name = "Məhsulların redaktəsi")]
        public async Task<JsonResult> CreateProduct(CreateProductCommand command)
        {
            return Json(await Mediator.Send(command));
        }

        [MyAuth]
        [HttpDelete(Name = "Məhsulların redaktəsi")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
        [MyAuth]
        [HttpGet(Name = "Məhsullar")]
        public async Task<IActionResult> ManageProductAccessories(int id)
        {
            ViewBag.Id = id;
            return View((IEnumerable<Product>)(await Mediator.Send(new GetAllAccessoriesQuery())).data);
        }
        [HttpGet]
        public async Task<JsonResult> GetProductAccessoriesByProductId(int id)
        {
            return Json(await Mediator.Send(new GetAllProductAccessoriesByProductIdQuery { Id = id }));
        }
        [HttpGet]
        public async Task<JsonResult> GetAccessories()
        {
            return Json(await Mediator.Send(new GetAllAccessoriesQuery()));
        }
        [HttpPost]
        public async Task<JsonResult> AddProductAccessory(AddProductAccessoryCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAccessory(int id, int accessoryId)
        {
            return Ok(await Mediator.Send(new DeleteProductAccessoryCommand { Id = id, accessoryId = accessoryId }));
        }
    }
}
