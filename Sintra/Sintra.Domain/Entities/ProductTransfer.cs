using System;

namespace Sintra.Domain.Entities
{
    public class ProductTransfer
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }

        public Warehouse FromWarehouse { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApproveDate { get; set; }
        public int TransferStatusId { get; set; }

        
    }
}