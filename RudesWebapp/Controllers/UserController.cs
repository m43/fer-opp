using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Data;
using RudesWebapp.Models;
using System.Threading.Tasks;

namespace RudesWebapp.Controllers
{
    public class UserController : Controller
    {
        private RudesDatabaseContext _context;
        private UserManager<User> _userManager;

        public UserController(RudesDatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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