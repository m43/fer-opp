using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class WebshopController : Controller
    {
        public IActionResult WebshopStart()
        {
            return View();
        }
    }
}