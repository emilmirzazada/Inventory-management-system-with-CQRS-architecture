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

namespace Sintra.Application.Features.CategoryAccessories.Commands.AddCategoryAccessory
{
    public partial class AddCategoryAccessoryCommand : IRequest<Response<string>>
    {
        public int categoryId { get; set; }
        public int accessoryId { get; set; }
        public class AddCategoryAccessoryCommandHandler : IRequestHandler<AddCategoryAccessoryCommand, Response<string>>
        {
            private readonly ICategoryAccessoryRepository CategoryAccessoryRepository;
            private readonly IMapper _mapper;
            public AddCategoryAccessoryCommandHandler(ICategoryAccessoryRepository CategoryAccessoryRepository, IMapper mapper)
            {
                this.CategoryAccessoryRepository = CategoryAccessoryRepository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(AddCategoryAccessoryCommand request, CancellationToken cancellationToken)
            {
                var CategoryAccessory = _mapper.Map<CategoryAccessory>(request);
                string a = CategoryAccessoryRepository.AddCategoryAccessory
                    (CategoryAccessory.CategoryId, CategoryAccessory.AccessoryId);

                return new Response<string>(a);
            }
        }
    }
    
}
