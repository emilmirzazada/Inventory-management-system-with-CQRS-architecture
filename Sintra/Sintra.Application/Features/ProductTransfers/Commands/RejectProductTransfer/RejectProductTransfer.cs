using MediatR;
using Sintra.Application.Interfaces.Repositories;
using Sintra.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sintra.Application.Features.ProductTransfers.Commands.RejectProductTransfer
{
    public class RejectProductTransferCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Message { get; set; }
        public class RejectProductTransferCommandHandler : IRequestHandler<RejectProductTransferCommand, Response<string>>
        {
            private readonly IProductTransferRepository _ProductTransferRepository;
            public RejectProductTransferCommandHandler(IProductTransferRepository ProductTransferRepository)
            {
                _ProductTransferRepository = ProductTransferRepository;
            }
            public async Task<Response<string>> Handle(RejectProductTransferCommand command, CancellationToken cancellationToken)
            {
                string result = await _ProductTransferRepository.RejectTransferProduct
                    (command.Id, command.StatusId, command.Message);
                return new Response<string>(result);
            }
        }
    }
}
