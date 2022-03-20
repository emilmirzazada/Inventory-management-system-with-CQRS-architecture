using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Users.Queries
{
    public class GetAllUsersViewModel : ApplicationUser
    {
        public string Edit
        {
            get
            {
                return $"<a href='/User/EditEmployee?id={Id}' id='{Id}' class='table-action-btn'><i class='md md-edit'></i></a>";
            }
        }
        public string Delete
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
        public string isBlock
        {
            get
            {
                if(IsBlocked)
                    return $@"<img style='width:20px;' src='/images/locked.svg'></img>";
                else
                    return $@"<img style='width:20px;' src='/images/unlocked.svg'></img>";

            }
        }
    }
}
