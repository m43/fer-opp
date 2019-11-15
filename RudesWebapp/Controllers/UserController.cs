using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    public class UserController : Controller
    {
        private User user;

        public UserController()
        {
           
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
