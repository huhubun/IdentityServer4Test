using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Admin.Controllers
{
    public class ApiResourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}