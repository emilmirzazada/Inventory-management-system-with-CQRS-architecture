using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public int RepeatDay { get; set; }
        public int CategoryId { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                await _productRepository.CreateProduct(product,request.CategoryId);
                return new Response<int>(1);
            }
            catch (System.Exception ex)
            {

                throw;
            }

            /*try
            {
                var product = _mapper.Map<Product>(request);
                await _productRepository.AddAsync(product);
                return new Response<int>(product.Id);
            }
            catch (System.Exception ex)
            {

                throw;
            }*/

        }
    }
}
