using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsite
{
    public static class AuthenticationCookie
    {
        public const string DAY_GUEST = "DayGuest";
        public const string EVENING_GUEST = "EveningGuest";
        //public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        //{
        //    await context.HttpContext.Authentication.SignInAsync("GuestCookie", 
        //        new System.Security.Claims.ClaimsPrincipal(), 
        //        new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
        //        {
        //            ExpiresUtc = DateTime.UtcNow.AddYears(2)
        //        });

        //    return;
        //}
    }
}
