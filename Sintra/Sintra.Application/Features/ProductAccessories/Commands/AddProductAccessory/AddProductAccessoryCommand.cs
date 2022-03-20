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

namespace Sintra.Application.Features.ProductAccessories.Commands.AddProductAccessory
{
    public partial class AddProductAccessoryCommand : IRequest<Response<string>>
    {
        public int parentId { get; set; }
        public int accessoryId { get; set; }
    }
    public class AddProductAccessoryCommandHandler : IRequestHandler<AddProductAccessoryCommand, Response<string>>
    {
        private readonly IProductAccessoryRepository ProductAccessoryRepository;
        private readonly IMapper _mapper;
        public AddProductAccessoryCommandHandler(IProductAccessoryRepository ProductAccessoryRepository, IMapper mapper)
        {
            this.ProductAccessoryRepository = ProductAccessoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddProductAccessoryCommand request, CancellationToken cancellationToken)
        {
            var productAccessory = _mapper.Map<ProductAccessory>(request);
            string a = ProductAccessoryRepository.AddProductAccessory
                (productAccessory.ParentId, productAccessory.AccessoryId);

            return new Response<string>(a);
        }
    }
}
