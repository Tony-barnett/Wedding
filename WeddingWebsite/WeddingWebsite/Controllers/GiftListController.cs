using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsite.Controllers
{
    [Authorize]
    public class GiftListController: Controller
    {
        //GET: Index
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}