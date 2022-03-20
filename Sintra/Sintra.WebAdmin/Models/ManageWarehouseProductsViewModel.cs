using Sintra.Application.Features.Products.Queries.GetAllProducts;
using Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Models
{
    public class ManageWarehouseProductsViewModel
    {
        public IEnumerable<GetAllProductsViewModel> Products { get; set; }
        public IEnumerable<GetAllWarehousesViewModel> Warehouses { get; set; }
    }
}
