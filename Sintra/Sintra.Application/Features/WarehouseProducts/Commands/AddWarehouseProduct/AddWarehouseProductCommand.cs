using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.AddWarehouseProduct
{
    public partial class AddWarehouseProductCommand : IRequest<Response<string>>
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Balance { get; set; }
    }
    public class AddWarehouseProductCommandHandler : IRequestHandler<AddWarehouseProductCommand, Response<string>>
    {
        private readonly IWarehouseProductRepository warehouseProductRepository;
        private readonly IMapper _mapper;
        public AddWarehouseProductCommandHandler(IWarehouseProductRepository warehouseProductRepository, IMapper mapper)
        {
            this.warehouseProductRepository = warehouseProductRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddWarehouseProductCommand request, CancellationToken cancellationToken)
        {
            var warehouseProduct = _mapper.Map<WarehouseProduct>(request);
            string a= warehouseProductRepository.AddWarehouseProduct
                (warehouseProduct.WarehouseId, warehouseProduct.ProductId, warehouseProduct.Balance);
            
            return new Response<string>(a);
        }
    }
}