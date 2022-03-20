using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Categories.Commands.CreateCategory;
using Sintra.Application.Features.Categories.Commands.DeleteCategory;
using Sintra.Application.Features.Categories.Commands.UpdateCategory;
using Sintra.Application.Features.Categories.Queries.GetAllCategories;
using Sintra.Application.Features.Categories.Queries.GetCategoryById;
using Sintra.Application.Features.CategoryAccessories.Commands.AddCategoryAccessory;
using Sintra.Application.Features.CategoryAccessories.Commands.DeleteCategoryAccessory;
using Sintra.Application.Features.CategoryAccessories.Queries.GetCategoryAccessories;
using Sintra.Application.Features.Products.Commands.DeleteProductById;
using Sintra.Application.Features.Products.Commands.UpdateProduct;
using Sintra.Application.Features.Products.Queries.GetAllAccessories;
using Sintra.Domain.Entities;
using Sintra.WebAdmin.StartupInjections.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class CategoryController : BaseController
    {
        [MyAuth]
        [HttpGet(Name = "Kateqoriyalar")]
        public async Task<IActionResult> Categories()
        {
            return View();
        }

        [MyAuth]
        [HttpGet(Name = "Kateqoriyalar")]
        public async Task<JsonResult> GetAllCategories()
        {
            return Json(await Mediator.Send(new GetAllCategoriesQuery()));
        }
        [MyAuth]
        [HttpGet(Name = "Kateqoriyalar")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }
        [MyAuth]
        [HttpPost(Name = "Kateqoriyaların redaktəsi")]
        public async Task<JsonResult> CreateCategory(CreateCategoryCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpPost(Name = "Kateqoriyaların redaktəsi")]
        public async Task<JsonResult> UpdateCategory(UpdateCategoryCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        [MyAuth]
        [HttpDelete(Name = "Kateqoriyaların redaktəsi")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryByIdCommand { Id = id }));
        }

        #region CategoryAccessories

        [MyAuth]
        [HttpGet(Name = "Kateqoriyalar")]
        public async Task<IActionResult> CategoryAccessories(int id)
        {
            ViewBag.id = id;
            return View((IEnumerable<Product>)(await Mediator.Send(new GetAllAccessoriesQuery())).data);
        }
        [MyAuth]
        [HttpGet(Name = "Kateqoriyalar")]
        public async Task<JsonResult> GetCategoryAccessories(int id)
        {
            return Json(await Mediator.Send(new GetCategoryAccessoriesQuery { Id = id }));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAccessory(int id, int accessoryId)
        {
            return Ok(await Mediator.Send(new DeleteCategoryAccessoryCommand { Id = id, accessoryId = accessoryId }));
        }
        [HttpPost]
        public async Task<JsonResult> AddCategoryAccessory(AddCategoryAccessoryCommand command)
        {
            return Json(await Mediator.Send(command));
        }
        #endregion
    }
}
