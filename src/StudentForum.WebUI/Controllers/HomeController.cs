﻿using Microsoft.AspNetCore.Mvc;

namespace StudentForum.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}