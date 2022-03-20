using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.CategoryAccessories.Commands.DeleteCategoryAccessory
{
    public class DeleteCategoryAccessoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int accessoryId { get; set; }
        public class DeleteCategoryAccessoryCommandHandler : IRequestHandler<DeleteCategoryAccessoryCommand, Response<int>>
        {
            private readonly ICategoryAccessoryRepository CategoryAccessoryRepository;
            public DeleteCategoryAccessoryCommandHandler(ICategoryAccessoryRepository CategoryAccessoryRepository)
            {
                this.CategoryAccessoryRepository = CategoryAccessoryRepository;
            }
            public async Task<Response<int>> Handle(DeleteCategoryAccessoryCommand command, CancellationToken cancellationToken)
            {
                CategoryAccessoryRepository.DeleteCategoryAccessory(command.Id, command.accessoryId);
                return new Response<int>(1);
            }
        }
    }
}
