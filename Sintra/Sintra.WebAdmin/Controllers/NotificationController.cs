using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class NotificationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
