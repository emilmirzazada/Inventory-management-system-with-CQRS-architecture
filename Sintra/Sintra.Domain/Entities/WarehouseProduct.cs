using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class WarehouseProduct
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Balance { get; set; }
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
    }
}
