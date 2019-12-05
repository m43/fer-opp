﻿using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Data;
using RudesWebapp.Models;

//[Authorize(Roles = "BoardMember")]
namespace RudesWebapp.Controllers
{
    public class BoardController : Controller
    {
        private User board_member;
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