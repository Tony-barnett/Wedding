using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WeddingPlanning.Controllers
{
    public class VenueController: Controller
    {
        public async Task<ActionResult> Index()
        {
            var guestId = GuestStore.Helpers.GetStorerIdFromCookie(Request);

            if(guestId == null)
            {
                throw new InvalidOperationException("Attempted to access stuff without being a valid guest");
            }

            var guest = await new GuestStore.GuestManager().GetGuest(guestId.Value);
            return View(guest);
        }
    }
}