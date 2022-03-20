using AutoMapper;
using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using Sintra.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Categories.Commands.CreateCategory
{
    public partial class CreateCategoryCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
        {
            private readonly ICategoryRepository CategoryRepository;
            private readonly IMapper _mapper;
            public CreateCategoryCommandHandler(ICategoryRepository CategoryRepository,
                IMapper mapper)
            {
                this.CategoryRepository = CategoryRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var Category = _mapper.Map<Category>(request);
                await CategoryRepository.AddAsync(Category);
                return new Response<int>(Category.Id);
            }
        }
    }

}
