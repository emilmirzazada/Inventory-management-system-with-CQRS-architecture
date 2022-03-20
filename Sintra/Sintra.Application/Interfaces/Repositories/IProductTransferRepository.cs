using Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer;
using Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface IProductTransferRepository : IGenericRepositoryAsync<ProductTransfer>
    {
        Task<ProductTransfer> GetProductTransferById(int id, string userId);
        Task<string> RejectTransferProduct(int id, int statusId, string message);
        string ApproveTransferProduct(int id, int statusId);
        void MakeNotificationViewed(int transferId, string userId);
        List<GetTransferProductsViewModel> GetTransferProducts(int transferId);
        void CreateProductTransfer(CreateProductTransferCommand command);
    }
}
