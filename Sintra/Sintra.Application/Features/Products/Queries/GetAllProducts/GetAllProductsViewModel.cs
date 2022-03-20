using Sintra.Domain.Entities;
using Sintra.Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintra.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Balance { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public string Type => ProductType.ProductTypes.Where(x => x.Id == ProductTypeId).FirstOrDefault()?.Value;
        public decimal RepeatDay { get; set; }
        public string RepeatDayy
        {
            get
            {
                return ProductTypeId == 2 ? RepeatDay.ToString() : "";
            }
        }
        
        public bool Deleted { get; set; }
        public string ManageProductAccessories
        {
            get
            {
                if (ProductTypeId == 1)
                    return $@"<a href = '/Product/ManageProductAccessories?id={Id}' id='{Id}' style='margin-left: 5px;' class='on-default edit-row'><i class='fa fa-pencil'></i></a>";
                return "";
            }
        }
        public string actions
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>
	                      <a href = '#' id='{Id}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
    }
}
