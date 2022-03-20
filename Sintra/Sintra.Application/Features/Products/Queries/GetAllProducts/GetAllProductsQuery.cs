using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<DatatableViewModel>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, DatatableViewModel>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _productRepository.GetAllProducts();
                IEnumerable<GetAllProductsViewModel> productsViewModel = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(products);
                return new DatatableViewModel { data = productsViewModel };
            }
            
        }
    }
    
}
