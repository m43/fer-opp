using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    public class UserController : Controller
    {
        private User user;
        private RudesDatabaseContext _context;

        public UserController(RudesDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult ViewData()
        {
            return View();
        }

        public IActionResult ChangeData(User user)
        {
            return View();
        }

        public IActionResult DeleteData()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AddReview(int ID_review)
        {
            return View();
        }

        public IActionResult BeginLivestream()
        {
            return View();
        }

    }
}
