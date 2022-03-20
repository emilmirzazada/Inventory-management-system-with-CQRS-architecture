using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductAccessories.Commands.DeleteProductAccessory
{
    public class DeleteProductAccessoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int accessoryId { get; set; }
        public class DeleteProductAccessoryCommandHandler : IRequestHandler<DeleteProductAccessoryCommand, Response<int>>
        {
            private readonly IProductAccessoryRepository ProductAccessoryRepository;
            public DeleteProductAccessoryCommandHandler(IProductAccessoryRepository ProductAccessoryRepository)
            {
                this.ProductAccessoryRepository = ProductAccessoryRepository;
            }
            public async Task<Response<int>> Handle(DeleteProductAccessoryCommand command, CancellationToken cancellationToken)
            {
                ProductAccessoryRepository.DeleteProductAccessory(command.Id, command.accessoryId);
                return new Response<int>(1);
            }
        }
    }
}
