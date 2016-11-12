using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingPlanning.Models;

namespace WeddingPlanning.Controllers
{
    public class RSVPController : Controller
    {
        // GET: RSVP
        public ActionResult RSVP()
        {
            var dropDownOptions = Enumerable.Range(0, 10).Select(thing => new SelectListItem { Text = thing.ToString(), Value = thing.ToString() }).ToList();
            ViewData["ChildrenViewModel"] = new ChildrenViewModel { DropDownItems = dropDownOptions };
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true };

            ViewBag.ReturnUrl = "#";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChildren()
        {

        }

        //GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CannotCome()
        {
            ViewBag.ReturnUrl = "#";
            return View(new GuestViewModel { IsComing = false });
        }
    }
}