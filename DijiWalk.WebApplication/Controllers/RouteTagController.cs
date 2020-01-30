using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DijiWalk.WebApplication.Controllers
{
    public class RouteTagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}