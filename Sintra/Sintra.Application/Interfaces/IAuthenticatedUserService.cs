using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
