using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class CoachController : Controller
    {
        //private Coach coach;
        public IActionResult Index()
        {
            return View();
        }

        //parametri fale
        public IActionResult AddPlayer()
        {
            return View();
        }

        public IActionResult ChangePlayerData(int ID_player)
        {
            return View();
        }

        public IActionResult DeletePlayer(int ID_player)
        {
            return View();
        }

        public IActionResult AddPost()
        {
            return View();
        }

        public IActionResult DeletePost(int ID_post)
        {
            return View();
        }

        public IActionResult ModifyPost(int ID_post)
        {
            return View();
        }
    }
}