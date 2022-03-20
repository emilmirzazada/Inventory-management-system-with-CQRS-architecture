using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.FeaturesMobile.Products.Queries.GetAllMobileProducts
{
    public class GetAllMobileProductsQuery : IRequest<DatatableViewModel>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllMobileProductsQuery, DatatableViewModel>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllMobileProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _productRepository.GetAllProducts();
                return new DatatableViewModel { data = products };
            }
            
        }
    }
    
}
