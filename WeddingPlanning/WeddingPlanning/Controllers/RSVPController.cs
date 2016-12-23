﻿using System;
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
            var storerId = GetStorerIdFromCookie();
            ViewData["ChildrenViewModel"] = new ChildrenViewModel { IsComing = true, AddedBy = storerId };
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true, AddedBy = storerId };

            var adults = _GuestManager.GetGuests(storedBy: storerId != null ? Guid.Parse(storerId.ToString()) : (Guid?)null);
            var children = _GuestManager.GetChildren(storedBy: storerId != null ? Guid.Parse(storerId.ToString()) : (Guid?)null);
            var people = new List<IPerson>();

            people.AddRange(adults);
            people.AddRange(children);
            
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
                storerId = GetStorerIdFromCookie();
            }
            await _GuestManager.AddGuest(guest, storerId);
            TempData["StorerId"] = guest.AddedBy;
            Response.SetCookie(new HttpCookie("storer", guest.AddedBy.ToString()));
            return RedirectToAction("/RSVP");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChildren(ChildrenViewModel child, Guid? storerId)
        {
            if(storerId == null)
            {
                storerId = GetStorerIdFromCookie();
            }
            await _GuestManager.AddChild(child, storerId.Value);
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

        public async Task<ActionResult> RemoveGuest(Guid id)
        {
            var guest = await _GuestManager.GetGuest(id);
            await _GuestManager.RemoveGuest(guest);
            return Redirect("/RSVP/RSVP");
        }

        public async Task<ActionResult> RemoveChild(Guid id)
        {
            var child = await _GuestManager.GetChild(id);
            await _GuestManager.RemoveChild(child);
            return Redirect("/RSVP/RSVP");
        }

        private Guid? GetStorerIdFromCookie()
        {
            var storer = Request.Cookies.Get("storer")?.Value;
            if (storer == null)
            {
                return null;
            }
            return Guid.Parse(storer);
        }
    }
}