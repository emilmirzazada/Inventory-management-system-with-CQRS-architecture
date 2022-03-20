using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Regions.Queries.GetAllRegions
{
    public class GetAllRegionsViewModel:Region
    {
        public string Edit
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string ManageRegionUsers
        {
            get
            {
                return $@"<a href = '/Region/ManageRegionUsers?Id={Id}' id='{Id}' style='margin-left: 5px;' class='manageRoleUsers on-default edit-row'><i class='fa fa-pencil'></i></a>";
            }
        }
    }
}
