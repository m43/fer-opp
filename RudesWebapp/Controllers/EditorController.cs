using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlayerEditor()
        {
            return View();
        }

        public IActionResult PostEditor()
        {
            return View();
        }

        public IActionResult ArticleEditor()
        {
            return View();
        }
    }
}