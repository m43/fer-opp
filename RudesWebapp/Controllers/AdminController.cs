using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    public class AdminController : Controller
    {
        private User admin;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewUsers()
        {
            return View();
        }

        public IActionResult DeleteUser(int ID_user)
        {
            return View();
        }

        public IActionResult DeleteReview(int ID_review)
        {
            return View();
        }
        

    }
}