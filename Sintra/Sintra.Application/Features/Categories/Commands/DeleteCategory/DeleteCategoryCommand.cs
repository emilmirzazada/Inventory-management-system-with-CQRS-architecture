using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Response<int>>
        {
            private readonly ICategoryRepository CategoryRepository;
            public DeleteCategoryByIdCommandHandler(ICategoryRepository CategoryRepository)
            {
                this.CategoryRepository = CategoryRepository;
            }
            public async Task<Response<int>> Handle(DeleteCategoryByIdCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var Category = await CategoryRepository.GetByIdAsync(command.Id);
                    if (Category == null) throw new Exception($"Category Not Found.");
                    await CategoryRepository.DeleteAsync(Category);
                    return new Response<int>(Category.Id);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
