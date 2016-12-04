using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingPlanning.GuestStore;
using WeddingPlanning.Models;

namespace WeddingPlanning.Controllers
{
    public class RSVPController : Controller
    {
        private GuestManager _GuestManager;
        public RSVPController() {
            _GuestManager = new GuestManager();
        }


        // GET: RSVP
        public ActionResult RSVP()
        {
            var storerId = ViewData["StorerId"];

            ViewData["ChildrenViewModel"] = new ChildrenViewModel { AddedBy= (int?)storerId };
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true, AddedBy= (int?)storerId };

            ViewBag.ReturnUrl = "/RSVP/AddGuest";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddGuest(GuestViewModel guest, int? storerId = null)
        {
            storerId = await _GuestManager.AddGuest(guest, storerId);
            ViewData["StorerId"] = storerId;
            return Redirect("/RSVP/RSVP");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChildren(ChildrenViewModel child, int storerId)
        {
            await _GuestManager.AddChild(child, storerId);
            return Redirect("/RSVP/RSVP");
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