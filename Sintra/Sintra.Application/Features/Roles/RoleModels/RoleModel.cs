using Microsoft.AspNetCore.Identity;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sintra.Application.Features.Roles.RoleModels
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleDetails
    {
        public IdentityRole<string> Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleEditModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }

    }

    public class GetAllRolesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ManageRoleClaims
        {
            get
            {
                return $@"<a href = '/Role/ManageRoleClaims?roleId={Id}' id='{Id}' style='margin-left: 5px;' class='manageRoleClaims on-default edit-row'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string ManageRoleUsers
        {
            get
            {
                return $@"<a href = '/Role/ManageRoleUsers?roleId={Id}' id='{Id}' style='margin-left: 5px;' class='manageRoleUsers on-default edit-row'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string Edit
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string Delete
        {
            get
            {
                return $@"<a href = '#' id='{Id}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
    }
}
