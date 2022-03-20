using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class CategoryAccessory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AccessoryId { get; set; }
        public Category Category { get; set; }
        public Product Accessory { get; set; }
    }
}
