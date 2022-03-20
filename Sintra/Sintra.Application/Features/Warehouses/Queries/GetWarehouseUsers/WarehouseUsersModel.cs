using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Warehouses.Queries.GetWarehouseUsers
{
    public class WarehouseUsersModel
    {
        public Warehouse Warehouse { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}
