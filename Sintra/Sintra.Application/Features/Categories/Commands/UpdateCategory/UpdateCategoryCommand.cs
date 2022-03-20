using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<int>>
        {
            private readonly ICategoryRepository _CategoryRepository;
            public UpdateCategoryCommandHandler(ICategoryRepository CategoryRepository)
            {
                _CategoryRepository = CategoryRepository;
            }
            public async Task<Response<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
            {
                var Category = await _CategoryRepository.GetByIdAsync(command.Id);

                if (Category == null)
                {
                    throw new Exception($"Category Not Found.");
                }
                else
                {
                    Category.Name = command.Name;
                    await _CategoryRepository.UpdateAsync(Category);
                    return new Response<int>(Category.Id);
                }
            }
        }
    }
}
