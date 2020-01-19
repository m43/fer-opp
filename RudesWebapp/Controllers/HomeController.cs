using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Seniors()
        {
            return View();
        }

        public IActionResult Juniors()
        {
            return View();
        }

        public IActionResult Cadets()
        {
            return View();
        }

        public IActionResult YoungCadets()
        {
            return View();
        }

        public IActionResult MiniBasketball()
        {
            return View();
        }

        public IActionResult SportSchools()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}