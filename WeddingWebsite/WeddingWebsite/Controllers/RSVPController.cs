using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.GuestStore;
using WeddingWebsite.Models;

namespace WeddingWebsite.Controllers
{
    [Authorize]
    public class RSVPController : Controller
    {
        private IGuestManager _GuestManager;

        public RSVPController(IGuestManager guestManager)
        {
            _GuestManager = guestManager;
        }

        // GET: RSVP
        public async Task<ActionResult> RSVP()
        {
            var storerId = GuestStore.Helpers.GetStorerIdFromCookie(Request.HttpContext);
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true, AddedBy = storerId };
            IEnumerable<IGuest> people = new List<IGuest>();
            if(storerId != null)
            {
                people = _GuestManager.GetGuestsStoredBy(storedBy: storerId.Value);
            }

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
                storerId = Helpers.GetStorerIdFromCookie(Request.HttpContext) ?? new Guid();
            }
            guest.AddedBy = storerId;
            await _GuestManager.AddGuest(guest);
            TempData["StorerId"] = guest.AddedBy;
            Response.Cookies.Append("storer", guest.AddedBy.ToString());
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
            if (Request.Cookies.Any(x => string.Compare("isComing", x.Key, true) == 0))
            {
                var value = Request.Cookies["isComing"];
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