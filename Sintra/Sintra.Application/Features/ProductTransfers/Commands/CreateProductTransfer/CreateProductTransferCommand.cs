using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer
{
    public partial class CreateProductTransferCommand : IRequest<Response<int>>
    {
        public int fromWarehouseId { get; set; }
        public int toWarehouseId { get; set; }
        public List<ProductQuantityModel> products { get; set; }
    }
    public class CreateProductTransferCommandHandler : IRequestHandler<CreateProductTransferCommand, Response<int>>
    {
        private readonly IProductTransferRepository _productTransferRepository;
        private readonly IMapper _mapper;
        public CreateProductTransferCommandHandler(IProductTransferRepository productTransferRepository, IMapper mapper)
        {
            _productTransferRepository = productTransferRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductTransferCommand request, CancellationToken cancellationToken)
        {
            _productTransferRepository.CreateProductTransfer(request);
            return new Response<int>(1);
        }
    }
}
