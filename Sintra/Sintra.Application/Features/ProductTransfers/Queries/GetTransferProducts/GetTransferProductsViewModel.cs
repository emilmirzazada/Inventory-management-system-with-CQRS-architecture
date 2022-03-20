using Sintra.Domain.Entities;
using Sintra.Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintra.Application.Features.ProductTransfers.Queries.GetTransferProducts
{
    public class GetTransferProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public string Type => ProductType.ProductTypes.Where(x => x.Id == ProductTypeId).FirstOrDefault()?.Value;
        public int RepeatDay { get; set; }
        public bool Deleted { get; set; }
        public int count { get; set; }
          
    }
}
