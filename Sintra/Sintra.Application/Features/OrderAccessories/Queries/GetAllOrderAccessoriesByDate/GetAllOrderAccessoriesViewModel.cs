using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.OrderAccessories.Queries.GetAllOrderAccessoriesByDate
{
    public class GetAllOrderAccessoriesViewModel:OrderAccessory
    {
        public string AddCall
        {
            get
            {
                return $@"<a href = '#' id='{Id}' data-toggle='modal' data-target='#editmodal' style='margin-left: 5px;' class='on-default edit-row editlink'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string UpdateAccessory
        {
            get
            {
                return $@"<a href = '#' id='{Id}' data-toggle='modal' data-target='#updateaccessorymodal' style='margin-left: 5px;' class='on-default edit-row updateaccessorylink'><i class='fa fa-pencil'></i></a>";
            }
        }
    }
}
