﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RudesWebapp.Controllers
{
    public class WebshopController : Controller
    {
        //private Webshop webshop;
        public IActionResult WebshopStart()
        {
            return View();
        }

        public IActionResult FindArticle(string ArticleName)
        {
            return View();
        }

        public IActionResult ShowFilters()
        {
            return View();
        }

        public IActionResult ShowArticles()
        {
            return View();
        }
    }
}