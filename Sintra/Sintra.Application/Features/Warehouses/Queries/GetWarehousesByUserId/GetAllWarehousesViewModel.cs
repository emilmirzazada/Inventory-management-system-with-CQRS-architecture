﻿using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Warehouses.Queries.GetAllWarehouses
{
    public class GetAllWarehousesViewModel:Warehouse
    {
        public string ManageWarehouseUsers
        {
            get
            {
                return $@"<a href = '/Warehouse/ManageWarehouseUsers?Id={Id}' id='{Id}' style='margin-left: 5px;' class='manageWarehouseUsers on-default edit-row'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string ManageWarehouseProducts
        {
            get
            {
                return $@"<a href = '/Warehouse/ManageWarehouseProducts?Id={Id}' style='margin-left: 5px;' class='manageWarehouseProducts on-default edit-row'><i class='fa fa-pencil'></i></a>";
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
