using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.ProductTransfers.Commands.ApproveProductTransfer
{
    public class ProductTransferViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public decimal FromPreviousBalance { get; set; }
        public decimal FromPresentBalance { get; set; }
        public decimal ToPreviousBalance { get; set; }
        public decimal ToPresentBalance { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApproveDate { get; set; }
        public int TransferStatusId { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public Warehouse ToWarehouse { get; set; }

        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Balance { get; set; }
        public int count { get; set; }
    }
}
