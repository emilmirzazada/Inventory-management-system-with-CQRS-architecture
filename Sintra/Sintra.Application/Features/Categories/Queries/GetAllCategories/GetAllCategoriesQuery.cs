using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<DatatableViewModel>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, DatatableViewModel>
        {
            private readonly ICategoryRepository catgeoryRepository;
            private readonly IMapper _mapper;
            public GetAllCategoriesQueryHandler(ICategoryRepository catgeoryRepository, IMapper mapper)
            {
                this.catgeoryRepository = catgeoryRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await catgeoryRepository.GetAsync<Category>();
                IEnumerable<GetAllCategoriesViewModel> categoriesViewModel = 
                    _mapper.Map<IEnumerable<GetAllCategoriesViewModel>>(categories).OrderByDescending(x=>x.Id);
                return new DatatableViewModel { data = categoriesViewModel };
            }

        }
    }
}
