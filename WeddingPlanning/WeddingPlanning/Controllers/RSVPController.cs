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
        public async Task<ActionResult> RSVP()
        {
            var storerId = GuestStore.Helpers.GetStorerIdFromCookie(Request);
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true, AddedBy = storerId };

            var people = _GuestManager.GetGuests(storedBy: storerId != null ? Guid.Parse(storerId.ToString()) : (Guid?)null);

            ViewBag.ReturnUrl = "/RSVP/AddGuest";
            ViewData["Guests"] = new Guests { AllGuests = people, storerId = storerId };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddGuest(GuestViewModel guest, Guid? storerId = null)
        {
            if(storerId == null)
            {
                storerId = GuestStore.Helpers.GetStorerIdFromCookie(Request);
            }
            await _GuestManager.AddGuest(guest, storerId);
            TempData["StorerId"] = guest.AddedBy;
            Response.SetCookie(new HttpCookie("storer", guest.AddedBy.ToString()));
            return RedirectToAction("/RSVP");
        }

        public async Task<ActionResult> EditGuest(Guid? guestId)
        {
            if (!guestId.HasValue)
            {
                return null;
            }
            var guest = await _GuestManager.GetGuest(guestId.Value);
            return PartialView("_Guest", guest as GuestViewModel);
        }

        //GET: Index
        public ActionResult Index()
        {
            if (Request.Cookies.AllKeys.Contains("isComing"))
            {
                var value = Request.Cookies["isComing"].Value;
                value = value.Substring(0, value.IndexOf(','));
                switch (value.ToLower()){
                    case "true":
                        return RedirectToAction("RSVP");
                    case "false":
                        return RedirectToAction("CannotCome");
                }
            }
            return View();
        }

        public ActionResult CannotCome()
        {
            ViewBag.ReturnUrl = "#";
            return View(new GuestViewModel { IsComing = false });
        }

        public async Task<ActionResult> RemoveGuest(Guid id)
        {
            var guest = await _GuestManager.GetGuest(id);
            await _GuestManager.RemoveGuest(guest);
            return Redirect("/RSVP/RSVP");
        }
    }
}