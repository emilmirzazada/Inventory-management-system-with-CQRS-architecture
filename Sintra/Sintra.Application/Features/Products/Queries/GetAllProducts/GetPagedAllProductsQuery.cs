using AutoMapper;
using MediatR;
using Sintra.Application.Filters;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Products.Queries.GetAllProducts
{
    public class GetPagedAllProductsQuery : IRequest<PagedResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPagedAllProductsQueryHandler : IRequestHandler<GetPagedAllProductsQuery, PagedResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetPagedAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllProductsViewModel>>> Handle(GetPagedAllProductsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllProductsParameter>(request);
            var product = await _productRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            product = product.Where(x => x.Deleted == false).ToList();
            var productViewModel = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(product);
            return new PagedResponse<IEnumerable<GetAllProductsViewModel>>(productViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
