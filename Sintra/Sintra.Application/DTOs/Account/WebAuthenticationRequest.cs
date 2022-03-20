using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.DTOs.Account
{
    public class WebAuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
