using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.StartupInjections.Authorization
{
    public class MyAuthAttribute : ServiceFilterAttribute
    {
        public MyAuthAttribute() : base(typeof(MyAuthFilter))
        {

        }
    }
}
