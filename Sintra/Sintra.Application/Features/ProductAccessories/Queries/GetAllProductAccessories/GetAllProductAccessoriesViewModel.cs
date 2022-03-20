using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.ProductAccessories.Queries.GetAllProductAccessories
{
    public class GetAllProductAccessoriesViewModel:ProductAccessory
    {
        public string actions
        {
            get
            {
                return $@"<a href = '#' id='{AccessoryId}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
    }
}
