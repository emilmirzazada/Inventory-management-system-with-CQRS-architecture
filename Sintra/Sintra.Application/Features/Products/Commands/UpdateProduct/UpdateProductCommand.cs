using MediatR;
using Sintra.Application.Exceptions;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int RepeatDay { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public UpdateProductCommandHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Response<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    throw new Exception($"Product Not Found.");
                }
                else
                {
                    product.Name = command.Name;
                    product.Barcode = command.Barcode;
                    product.Description = command.Description;
                    product.ProductTypeId = command.ProductTypeId;
                    product.RepeatDay = command.RepeatDay;
                    product.Price = command.Price;
                    await _productRepository.UpdateAsync(product);
                    return new Response<int>(product.Id);
                }
            }
        }
    }
}
