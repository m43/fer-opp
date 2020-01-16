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