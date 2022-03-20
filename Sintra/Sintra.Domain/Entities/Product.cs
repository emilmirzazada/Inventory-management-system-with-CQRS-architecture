using Sintra.Domain.Common;
using Sintra.Domain.Statuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public int RepeatDay { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}