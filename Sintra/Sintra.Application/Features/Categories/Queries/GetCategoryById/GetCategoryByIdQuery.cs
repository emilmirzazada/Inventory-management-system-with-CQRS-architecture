using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Response<Category>>
    {
        public int Id { get; set; }
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<Category>>
        {
            private readonly ICategoryRepository _CategoryRepository;
            public GetCategoryByIdQueryHandler(ICategoryRepository CategoryRepository)
            {
                _CategoryRepository = CategoryRepository;
            }
            public async Task<Response<Category>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var Category = await _CategoryRepository.GetByIdAsync<Category>(query.Id);
                if (Category == null) throw new Exception($"Category Not Found.");
                return new Response<Category>(Category);
            }
        }
    }
}
