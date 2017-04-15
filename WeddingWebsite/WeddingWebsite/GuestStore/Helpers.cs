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
            var storer = request.User.Claims.SingleOrDefault( x => string.Compare(x.Type, "GuestId", true) == 0).Value;
            if (storer == null)
            {
                return null;
            }
            return Guid.Parse(storer);
        }

    }
}