using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class TransferProducts
    {
        public int TransferId { get; set; }
        public int ProductId { get; set; }
        public int count { get; set; }
        public decimal FromPreviousBalance { get; set; }
        public decimal FromPresentBalance { get; set; }
        public decimal? ToPreviousBalance { get; set; }
        public decimal? ToPresentBalance { get; set; }
        public ProductTransfer Transfer { get; set; }
        public Product Product { get; set; }
    }
}
