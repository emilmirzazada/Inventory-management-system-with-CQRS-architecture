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

namespace Sintra.Application.Features.Warehouses.Commands.CreateWarehouse
{
    public partial class CreateWarehouseCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
    }
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Response<int>>
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IMapper _mapper;
        public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            this.warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var warehouse = _mapper.Map<Warehouse>(request);
                await warehouseRepository.AddAsync(warehouse);
                return new Response<int>(warehouse.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
