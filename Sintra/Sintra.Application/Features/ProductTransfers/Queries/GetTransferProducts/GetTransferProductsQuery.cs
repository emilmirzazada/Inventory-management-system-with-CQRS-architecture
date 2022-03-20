using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts
{
    public class GetTransferProductsQuery : IRequest<DatatableViewModel>
    {
        public int transferId { get; set; }
        public string userId { get; set; }
        public class GetTransferProductsQueryHandler : IRequestHandler<GetTransferProductsQuery, DatatableViewModel>
        {
            private readonly IProductTransferRepository productTransferRepository;
            private readonly IMapper _mapper;
            public GetTransferProductsQueryHandler(IProductTransferRepository productTransferRepository, IMapper mapper)
            {
                this.productTransferRepository = productTransferRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetTransferProductsQuery request, CancellationToken cancellationToken)
            {
                var products = productTransferRepository.GetTransferProducts(request.transferId);
                productTransferRepository.MakeNotificationViewed(request.transferId, request.userId);
                return new DatatableViewModel { data = products.OrderByDescending(x => x.Id) };
            }

        }
    }
}
