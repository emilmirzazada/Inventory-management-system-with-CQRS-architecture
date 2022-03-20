using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesViewModel : Category
    {
        public string accessories
        {
            get
            {
                return $@"<a href = '/Category/CategoryAccessories?id={Id}' id='{Id}' style='margin-left: 5px;' class='on-default edit-row'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string actions
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>";
            }
        }
    }
}
