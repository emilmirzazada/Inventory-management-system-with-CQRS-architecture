using Sintra.Application.Features.ProductTransfers.Commands.CreateProductTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.PostBodyModels
{
    public class CreateTransferData
    {
        public List<ProductQuantityModel> products { get; set; }
        public int fromWarehouseId { get; set; }
        public int toWarehouseId { get; set; }
    }
}
