using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Warehouses.Commands.UpdateWarehouse
{
    public class UpdateWarehouseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }

        public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Response<int>>
        {
            private readonly IWarehouseRepository _WarehouseRepository;
            public UpdateWarehouseCommandHandler(IWarehouseRepository WarehouseRepository)
            {
                _WarehouseRepository = WarehouseRepository;
            }
            public async Task<Response<int>> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
            {
                var Warehouse = await _WarehouseRepository.GetByIdAsync(command.Id);

                if (Warehouse == null)
                {
                    throw new Exception($"Warehouse Not Found.");
                }
                else
                {
                    Warehouse.Name = command.Name;
                    Warehouse.Phonenumber = command.Phonenumber;
                    Warehouse.Address = command.Address;
                    await _WarehouseRepository.UpdateAsync(Warehouse);
                    return new Response<int>(Warehouse.Id);
                }
            }
        }
    }
}
