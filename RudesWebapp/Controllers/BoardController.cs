using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class BoardController : Controller
    {
        //private Board board;
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