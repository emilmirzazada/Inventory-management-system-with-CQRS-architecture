using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Products.Queries.GetAllAccessories
{
    public class GetAllAccessoriesQuery : IRequest<DatatableViewModel>
    {
        public class GetAllAccessoriesQueryHandler : IRequestHandler<GetAllAccessoriesQuery, DatatableViewModel>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;
            public GetAllAccessoriesQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllAccessoriesQuery request, CancellationToken cancellationToken)
            {
                var products = _productRepository.Get<Product,Product>(x=>x.ProductTypeId==2 && x.Deleted==false);
                return new DatatableViewModel { data = products };
            }

        }
    }
}
