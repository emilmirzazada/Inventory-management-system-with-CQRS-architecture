using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.StartupInjections
{
    public class MyApiAuthAttribute : ServiceFilterAttribute
    {
        public MyApiAuthAttribute() : base(typeof(MyApiAuthFilter))
        {

        }
    }
}
