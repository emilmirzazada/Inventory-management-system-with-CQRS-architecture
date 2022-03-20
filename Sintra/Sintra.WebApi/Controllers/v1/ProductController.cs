using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.Features.Products.Commands.CreateProduct;
using Sintra.Application.Features.Products.Commands.DeleteProductById;
using Sintra.Application.Features.Products.Commands.UpdateProduct;
using Sintra.Application.FeaturesMobile.Products.Queries.GetAllMobileProducts;
using Sintra.Application.Features.Products.Queries.GetProductById;
using Sintra.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sintra.Application.Features.Products.Queries.GetAllProducts;

namespace Sintra.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsParameter filter)
        {

            return Ok(await Mediator.Send(new GetPagedAllProductsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }
        //GetAllProducts
        [HttpGet("GetAllProducts")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {

            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }
        // GET api/<controller>/5

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
    }
}
