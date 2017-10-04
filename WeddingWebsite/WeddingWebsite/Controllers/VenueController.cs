using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsite.GuestStore;

namespace WeddingWebsite.Controllers
{
    [Authorize]
    public class VenueController: Controller
    {
        private IGuestManager _GuestManager;
        public VenueController(IGuestManager guestManager)
        {
            _GuestManager = guestManager;
        }

        public async Task<ActionResult> Index()
        {
            var guestId = Helpers.GetStorerIdFromCookie(Request.HttpContext);

            if(guestId == null)
            {
                throw new InvalidOperationException("Attempted to access stuff without being a valid guest");
            }

            var guest = await _GuestManager.GetGuest(guestId.Value);
            return View(guest);
        }
    }
}