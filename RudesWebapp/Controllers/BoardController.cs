using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Data;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = "Admin, Board")]
    public class BoardController : Controller
    {
        private RudesDatabaseContext _context;

        public BoardController(RudesDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewActiveOrders()
        {
            return View();
        }

        public IActionResult ReceiveOrder(int ID_order)
        {
            return View();
        }
    }
}