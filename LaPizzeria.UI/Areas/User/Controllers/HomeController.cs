﻿using Microsoft.AspNetCore.Mvc;

namespace LaPizzeria.UI.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
