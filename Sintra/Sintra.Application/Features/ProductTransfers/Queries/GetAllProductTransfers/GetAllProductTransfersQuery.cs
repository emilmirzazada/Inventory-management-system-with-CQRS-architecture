using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Features.ProductTransfers.Queries.GetAllProductTransfers;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransferTransfers.Queries.GetAllProductTransferTransfers
{
    public class GetAllProductTransfersQuery : IRequest<DatatableViewModel>
    {
        public class GetAllProductTransfersQueryHandler : IRequestHandler<GetAllProductTransfersQuery, DatatableViewModel>
        {
            private readonly IProductTransferRepository _ProductTransferRepository;
            public GetAllProductTransfersQueryHandler(IProductTransferRepository ProductTransferRepository)
            {
                _ProductTransferRepository = ProductTransferRepository;
            }

            public async Task<DatatableViewModel> Handle(GetAllProductTransfersQuery request, CancellationToken cancellationToken)
            {
                var ProductTransfers = _ProductTransferRepository.Get<ProductTransfer, GetAllProductTransfersViewModel>
                                        (includeProperties: "FromWarehouse,ToWarehouse");
                return new DatatableViewModel { data = ProductTransfers.OrderByDescending(x => x.Id) };
            }

        }
    }
}
