using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransfers.Queries.GetProductTransferById
{
    public class GetProductTransferByIdQuery : IRequest<ProductTransfer>
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public class GetProductTransferByIdQueryHandler : IRequestHandler<GetProductTransferByIdQuery, ProductTransfer>
        {
            private readonly IProductTransferRepository productTransferRepository;
            public GetProductTransferByIdQueryHandler(IProductTransferRepository productTransferRepository)
            {
                this.productTransferRepository = productTransferRepository;
            }
            public async Task<ProductTransfer> Handle(GetProductTransferByIdQuery query, CancellationToken cancellationToken)
            {
                var productTransfer = await productTransferRepository.GetProductTransferById(query.Id,query.userId);
                productTransferRepository.MakeNotificationViewed(query.Id, query.userId);
                if (productTransfer == null) throw new Exception($"Product Transfer Not Found.");
                return productTransfer;
            }
        }
    }
}
