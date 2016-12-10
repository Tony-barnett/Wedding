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
        public ActionResult RSVP()
        {
            var storerId = GetStorerIdFromCookie();
            ViewData["ChildrenViewModel"] = new ChildrenViewModel { AddedBy= storerId };
            ViewData["GuestViewModel"] = new GuestViewModel { IsComing = true, AddedBy= (int?)storerId };
            var adults = _GuestManager.GetGuests(storedBy: storerId != null? int.Parse(storerId.ToString()): (int?)null);
            ViewBag.ReturnUrl = "/RSVP/AddGuest";
            ViewData["Guests"] = new Guests { AllGuests = adults.Result };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddGuest(GuestViewModel guest, int? storerId = null)
        {
            if(storerId == null)
            {
                storerId = GetStorerIdFromCookie();
            }
            storerId = await _GuestManager.AddGuest(guest, storerId);
            TempData["StorerId"] = storerId;
            Response.SetCookie(new HttpCookie("storer", storerId.Value.ToString()));
            return RedirectToAction("/RSVP");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChildren(ChildrenViewModel child, int? storerId)
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

        private int? GetStorerIdFromCookie()
        {
            var storer = Request.Cookies.Get("storer")?.Value;
            if (storer == null)
            {
                return null;
            }
            return int.Parse(storer);
        }
    }
}