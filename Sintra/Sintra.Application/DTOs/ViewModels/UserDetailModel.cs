using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.DTOs.ViewModels
{
    public class UserDetailModel
    {
        public string UserId { get; set; }
        public bool isBlocked { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
