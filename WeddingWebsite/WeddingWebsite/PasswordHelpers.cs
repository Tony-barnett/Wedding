using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WeddingWebsite.Models;
using WeddingPlanning.Models;

namespace WeddingWebsite
{
        public class PasswordHelpers
    {
        private IConfiguration _Configuration;

        public PasswordHelpers(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string DayGuestPassword
        {
            get
            {
                if (_DayGuestPassword == null)
                {
                    _DayGuestPassword = _Configuration.GetConfigurationValue<string>("DayGuestPassword");
                }
                return _DayGuestPassword;
            }
        }
        internal string _DayGuestPassword { get; set; }

        public string EveningGuestPassword
        {
            get
            {
                if (_EveningGuestPassword == null)
                {
                    _EveningGuestPassword = _Configuration.GetConfigurationValue<string>("EveningGuestPassword");
                }
                return _EveningGuestPassword;
            }
        }

        internal string _EveningGuestPassword { get; set; }

        public GuestType GetGuestTypeFromPassword(string password)
        {
            if (DayGuestPassword == password)
            {
                return GuestType.Day;
            }

            if (EveningGuestPassword == password)
            {
                return GuestType.Evening;
            }

            return GuestType.None;
        }

        public async Task WriteGuestTypeCookieAsync(GuestType guestType, IList<Claim> claims, HttpContext httpContext)
        {
            claims.Add(new Claim("GuestType", guestType.ToString()));
            claims.Add(new Claim("GuestId", Guid.NewGuid().ToString()));


            var id = new ClaimsIdentity(claims, "local");
            await httpContext.Authentication.SignInAsync("NomNomNom", new ClaimsPrincipal(id));
        }
    }
}
