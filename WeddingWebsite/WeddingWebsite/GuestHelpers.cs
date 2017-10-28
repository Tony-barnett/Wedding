using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeddingWebsite.Models;

namespace WeddingWebsite
{
    public static class GuestHelpers
    {
        public static GuestType GetGuestType(ClaimsPrincipal user)
        {
            if (Enum.TryParse(user.Claims.Single(x => x.Type == "GuestType").Value, out GuestType guestType))
            {
                return guestType;
            }
            return GuestType.None;
        }
    }
}
