using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingPlanning.Models;
using WeddingPlanning.StuffStorage;

namespace WeddingPlanning.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {

        get
            {
                if (_signInManager == null)
                {
                    var foo = HttpContext.GetOwinContext();

                    _signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>("");
                }
                return _signInManager;
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnterPassword(string ReturnUrl)
        {
            return View(new RedirectModel {ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnterPasswordCallback(RedirectModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var guestType = GuestStore.Helpers.GetGuestTypeFromPassword(model.Password);
            var guestTypeClaim = new Claim("GuestType", guestType.ToString());
            var guestId = new Claim("GuestId", Guid.NewGuid().ToString());

            var identity = new ClaimsIdentity(new[] { guestTypeClaim, guestId }, DefaultAuthenticationTypes.ApplicationCookie);
            
            var guestManager = new GuestStore.GuestManager();
            var user = new ApplicationUser();
            
            await SignInManager.SignInAsync(new ApplicationUser(), true, true);
            
            //var newGuest = new GuestViewModel { GuestType = guestType };

            //await guestManager.AddGuest(newGuest);
            IPasswordMapperRepository mapWriter = new CSVStorer();
                        
            return Redirect(model.ReturnUrl);
        }
    }
}