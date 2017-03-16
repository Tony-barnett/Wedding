using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WeddingPlanning.Models;

namespace WeddingPlanning.GuestStore
{
    public static class Helpers
    {
        public static string DayGuestPassword
        {
            get
            {
                if (_DayGuestPassword == null)
                {
                    _DayGuestPassword = ConfigurationManager.AppSettings["DayGuestPassword"];
                }
                return _DayGuestPassword;
            }
        }
        internal static string _DayGuestPassword { get; set; }

        public static string EveningGuestPassword
        {
            get
            {
                if (_EveningGuestPassword == null)
                {
                    _EveningGuestPassword = ConfigurationManager.AppSettings["EveningGuestPassword"];
                }
                return _EveningGuestPassword;
            }
        }
        internal static string _EveningGuestPassword { get; set; }
            
        public static Guid? GetStorerIdFromCookie(HttpRequestBase request)
        {
            var storer = request.Cookies.Get("storer")?.Value;
            if (storer == null)
            {
                return null;
            }
            return Guid.Parse(storer);
        }


        public static GuestType GetGuestTypeFromPassword(string password)
        {
            if(DayGuestPassword == password)
            {
                return GuestType.Day;
            }

            if(EveningGuestPassword == password)
            {
                return GuestType.Evening;
            }

            return GuestType.None;
        }
    }
}