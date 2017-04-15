using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WeddingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationCookie.DAY_GUEST)]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize(AuthenticationCookie.EVENING_GUEST)]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
