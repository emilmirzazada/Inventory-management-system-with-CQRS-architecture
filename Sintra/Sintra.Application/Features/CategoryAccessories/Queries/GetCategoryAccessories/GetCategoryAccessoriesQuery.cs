using AutoMapper;
using MediatR;
using Sintra.Application.DTOs.Datatable;
using Sintra.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.CategoryAccessories.Queries.GetCategoryAccessories
{
    public class GetCategoryAccessoriesQuery : IRequest<DatatableViewModel>
    {
        public int Id { get; set; }
        public class GetCategoryAccessoriesQueryHandler : IRequestHandler<GetCategoryAccessoriesQuery, DatatableViewModel>
        {
            private readonly ICategoryAccessoryRepository CategoryAccessoryRepository;
            private readonly IMapper _mapper;
            public GetCategoryAccessoriesQueryHandler(ICategoryAccessoryRepository CategoryAccessoryRepository,
                IMapper mapper)
            {
                this.CategoryAccessoryRepository = CategoryAccessoryRepository;
                _mapper = mapper;
            }

            public async Task<DatatableViewModel> Handle(GetCategoryAccessoriesQuery request, CancellationToken cancellationToken)
            {
                var CategoryAccessorys = await CategoryAccessoryRepository.GetCategoryAccessories(request.Id);
                IEnumerable<GetCategoryAccessoriesViewModel> CategoryAccessoriesViewModel = 
                    _mapper.Map<IEnumerable<GetCategoryAccessoriesViewModel>>(CategoryAccessorys).OrderByDescending(x=>x.Id);
                return new DatatableViewModel { data = CategoryAccessoriesViewModel };
            }

        }
    }
}
