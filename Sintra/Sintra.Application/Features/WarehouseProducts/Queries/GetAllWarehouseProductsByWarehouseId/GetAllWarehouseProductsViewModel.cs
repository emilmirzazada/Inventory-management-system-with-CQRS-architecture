using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Warehouses.Queries.GetAllWarehouseProducts
{
    public class GetAllWarehouseProductsViewModel:WarehouseProduct
    {
        public string actions
        {
            get
            {
                return $@"<a href = '#' id='{ProductId}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>
	                      <a href = '#' id='{ProductId}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
        public string transfer
        {
            get
            {
                return $@"<input class='productcountinput' id='{ProductId}' data-frwid='{WarehouseId}' type='number' min='1' max='{Balance}' style='width: 100 %; text - align: center; '>";
            }
        }
    }
}