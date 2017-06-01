using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Owin;
using Microsoft.Owin;

namespace WeddingPlanning.Middleware
{
    public delegate Task RequestDelegate(HttpContext context);

    public class PasswordMiddleware : OwinMiddleware
    {

        public PasswordMiddleware(OwinMiddleware next) : base(next)
        {

        }

        public async override Task Invoke(IOwinContext context)
        {

            if (context.Request.Cookies["storer"] == null
                && string.Compare(context.Request.Path.Value, "/home/enterpassword", true) != 0
                && string.Compare(context.Request.Path.Value, "/home/EnterPasswordCallback", true) != 0)
            {

                context.Response.Redirect($"/Home/EnterPassword?referrer={HttpUtility.UrlEncode(context.Request.Path.Value)}");
                return;
            }

            await Next.Invoke(context);
        }
    }

    public static class PasswordMiddlewareExtensions
    {
        public static IAppBuilder UsePasswordMiddleware(this IAppBuilder app)
        {
            return app.Use<PasswordMiddleware>();
        }
    }
}