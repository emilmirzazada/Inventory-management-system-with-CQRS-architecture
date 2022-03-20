using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public static class ProductType
    {
        public static readonly List<Status> ProductTypes;
        static ProductType()
        {
            Product = new Status
            {
                Id = 1,
                Value = "Məhsul"
            };
            Accessory = new Status
            {
                Id = 2,
                Value = "Aksessuar"
            };
            ProductTypes = new List<Status>();
            ProductTypes.Add(Product);
            ProductTypes.Add(Accessory);
        }
        public static readonly Status Product;
        public static readonly Status Accessory;

    }
}
