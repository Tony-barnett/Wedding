using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingPlanning.GuestStore
{
    public static class Helpers
    {
        public static Guid? GetStorerIdFromCookie(HttpRequestBase request)
        {
            var storer = request.Cookies.Get("storer")?.Value;
            if (storer == null)
            {
                return null;
            }
            return Guid.Parse(storer);
        }
    }
}