using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Users.Commands.EditUser
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        public bool isBlocked { get; set; }
        public decimal MinAmount { get; set; }
        public decimal Percentage { get; set; }
        public decimal MaxAmount { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
        public IEnumerable<string> AllRoles { get; set; }
    }
}
