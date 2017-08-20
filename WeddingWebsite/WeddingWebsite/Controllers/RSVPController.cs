using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            IEnumerable<GuestViewModel> people = new List<GuestViewModel>();
            if (storerId != null)
            {
                people = _GuestManager.GetGuestsStoredBy(storedBy: storerId.Value);
            }

            ViewBag.ReturnUrl = "/RSVP/AddGuest";
            ViewData["Guests"] = new Guests { AllGuests = people, storerId = storerId };
            return View();
        }

        // GET: RSVP
        public async Task<ActionResult> RSVP2()
        {
            return View();
        }

        public async Task<JsonResult> GetStoredGuests(bool? isComing = null)
        {
            var guests = new List<GuestViewModel>();


            if (User.Claims.Any(x => x.Type == "GuestId"))
            {
                Guid.TryParse(User.Claims.Single(x => x.Type == "GuestId").Value, out Guid id);
                var g = _GuestManager.GetGuestsStoredBy(id);
                if(isComing != null)
                {
                    g = g.Where(x => x.IsComing == isComing);
                }
                guests = g.ToList();
            }

            return Json(guests);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddGuest(GuestViewModel guest, Guid? storerId = null)
        //{
        //    if (storerId == null)
        //    {
        //        storerId = Helpers.GetStorerIdFromCookie(Request.HttpContext) ?? new Guid();
        //    }
        //    guest.AddedBy = storerId;
        //    await _GuestManager.AddGuest(guest);
        //    Response.Cookies.Append("storer", guest.AddedBy.ToString());
        //    return RedirectToAction("/RSVP");
        //}

        private struct NewGuestResponse
        {
            public string Result { get; set; }
            public Guid GuestId { get; set; }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> AddGuest([FromBody]GuestViewModel guest)
        {
            try
            {
                Enum.TryParse(User.Claims.Single(x => x.Type == "GuestType").Value, out GuestType guestType);
                Guid.TryParse(User.Claims.Single(x => x.Type == "GuestId").Value, out Guid id);

                guest.AddedBy = id;
                guest.GuestType = guestType;

                await _GuestManager.AddGuest(guest);
            }
            catch
            {
                return Json("Fail");
            }
            return Json(
                new NewGuestResponse {
                    Result = "Success",
                    GuestId = guest.Id.Value
                });
        }

        [HttpDelete]
        public async Task<JsonResult> RemoveGuest(Guid guestId)
        {
            try
            {
                var guest = await _GuestManager.GetGuest(guestId);
                await _GuestManager.RemoveGuest(guest);
            }
            catch (Exception e)
            {
                return Json("Fail");
            }
            return Json("Success");
        }

        [HttpPut]
        public async Task<ActionResult> EditGuest([FromBody]GuestViewModel guest)
        {
            try
            {
                Enum.TryParse(User.Claims.Single(x => x.Type == "GuestType").Value, out GuestType guestType);
                Guid.TryParse(User.Claims.Single(x => x.Type == "GuestId").Value, out Guid id);

                guest.AddedBy = id;
                guest.GuestType = guestType;

                await _GuestManager.EditGuest(guest);
            }
            catch
            {
                return Json("Fail");
            }
            return Json("Success");
            
        }

        //public async Task<ActionResult> EditGuest(Guid? guestId)
        //{
        //    if (!guestId.HasValue)
        //    {
        //        return null;
        //    }
        //    var guest = await _GuestManager.GetGuest(guestId.Value);
        //    return PartialView("_Guest", guest as GuestViewModel);
        //}

        //GET: Index
        public ActionResult Index()
        {
            //if (Request.Cookies.Any(x => string.Compare("isComing", x.Key, true) == 0))
            //{
            //    var value = Request.Cookies["isComing"];
            //    value = value.Substring(0, value.IndexOf(','));
            //    switch (value.ToLower()){
            //        case "true":
            //            return RedirectToAction("RSVP");
            //        case "false":
            //            return RedirectToAction("CannotCome");
            //    }
            //}
            return View();
        }

        //public ActionResult CannotCome()
        //{
        //    ViewBag.ReturnUrl = "#";
        //    return View(new GuestViewModel { IsComing = false });
        //}

        //public async Task<ActionResult> RemoveGuest(Guid id)
        //{
        //    var guest = await _GuestManager.GetGuest(id);
        //    await _GuestManager.RemoveGuest(guest);
        //    return Redirect("/RSVP/RSVP");
        //}
    }
}