using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WeddingWebsite.GuestStore;

namespace WeddingWebsite.Controllers
{
    public class QAndAController: Controller
    {
        //GET: Index
        public ActionResult Index()
        {
            Helpers.GetStorerIdFromCookie(Request.HttpContext);
            return View();
        }
    }
}