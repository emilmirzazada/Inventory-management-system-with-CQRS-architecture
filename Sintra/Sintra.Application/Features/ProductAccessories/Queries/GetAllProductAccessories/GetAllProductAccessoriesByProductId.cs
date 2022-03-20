using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductAccessories.Queries.GetAllProductAccessories
{
    public class GetAllProductAccessoriesByProductIdQuery : IRequest<DatatableViewModel>
    {
        public int Id { get; set; }
        public class GetAllProductAccessorysByProductIdQueryHandler : IRequestHandler<GetAllProductAccessoriesByProductIdQuery, DatatableViewModel>
        {
            private readonly IProductAccessoryRepository ProductAccessoryRepository;
            private readonly IMapper _mapper;
            public GetAllProductAccessorysByProductIdQueryHandler(IProductAccessoryRepository ProductAccessoryRepository,
                IMapper mapper)
            {
                this.ProductAccessoryRepository = ProductAccessoryRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllProductAccessoriesByProductIdQuery request, CancellationToken cancellationToken)
            {
                var ProductAccessorys = await ProductAccessoryRepository.GetAllProductAccessoriesByProductId(request.Id);
                IEnumerable<GetAllProductAccessoriesViewModel> productAccessoriesViewModel = _mapper.Map<IEnumerable<GetAllProductAccessoriesViewModel>>(ProductAccessorys);
                return new DatatableViewModel { data = productAccessoriesViewModel };
            }

        }
    }
}
