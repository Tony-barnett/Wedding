using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeddingWebsite.Controllers
{
    [Authorize]
    public class ItineraryController : Controller
    {
        //GET: Index
        public ActionResult Index()
        {
            return View();
        }
    }
}