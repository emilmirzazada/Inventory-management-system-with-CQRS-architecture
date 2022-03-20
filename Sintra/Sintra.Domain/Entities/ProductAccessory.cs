using System.Collections.Generic;

namespace Sintra.Domain.Entities
{
    public class ProductAccessory
    {
        public int AccessoryId { get; set; }
        public int ParentId { get; set; }
        public Product Accessory { get; set; }
        public Product Parent { get; set; }
    }
}
