using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace Sintra.Application.DTOs.USerClaims
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Edit Product","Edit Product"),
            new Claim("Edit Warehouse","Edit Warehouse"),
            new Claim("Edit User","Edit User"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Product","Delete Product"),
            new Claim("Delete Warehouse","Delete Warehouse"),
            new Claim("Delete User","Delete User"),
            new Claim("ManageWarehouseUser","ManageWarehouseUser"),
            new Claim("ManageWarehouseProduct","ManageWarehouseProduct")
        };

    }
}
