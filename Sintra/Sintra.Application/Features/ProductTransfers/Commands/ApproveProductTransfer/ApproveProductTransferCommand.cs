using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransfers.Commands.ApproveProductTransfer
{
    public class ApproveProductTransferCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public class ApproveProductTransferCommandHandler : IRequestHandler<ApproveProductTransferCommand, Response<string>>
        {
            private readonly IProductTransferRepository _ProductTransferRepository;
            public ApproveProductTransferCommandHandler(IProductTransferRepository ProductTransferRepository)
            {
                _ProductTransferRepository = ProductTransferRepository;
            }
            public Task<Response<string>> Handle(ApproveProductTransferCommand command, CancellationToken cancellationToken)
            {
                string result = _ProductTransferRepository.ApproveTransferProduct(command.Id,command.StatusId);
                return Task.FromResult(new Response<string>(result));
            }
        }
    }
}