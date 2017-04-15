using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using WeddingWebsite.Models;

namespace WeddingWebsite.GuestStore
{
    public static class Helpers
    {
        public static Guid? GetStorerIdFromCookie(HttpContext request)
        {
            var storer = request.Request.Cookies["storer"];
            if (storer == null)
            {
                return null;
            }
            return Guid.Parse(storer);
        }

    }
}