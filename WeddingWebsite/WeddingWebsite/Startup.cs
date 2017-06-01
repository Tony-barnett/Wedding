using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeddingWebsite.Data;
using WeddingWebsite.Models;
using WeddingWebsite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Practices.Unity;
using WeddingWebsite.GuestStore;
using WeddingWebsite.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace WeddingWebsite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);



            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthorization(options =>
                options.AddPolicy("DayGuest",
                    policy => policy.RequireClaim("GuestType", "Day")
            ));

            services.AddAuthorization(options =>
                options.AddPolicy("EveningGuest",
                    policy => policy.RequireClaim("GuestType", "Evening", "Day")
            ));

            services.AddAuthorization(options =>
                options.AddPolicy("IsLoggedIn",
                policy => policy.RequireClaim("GuestId")
            ));

            InjectDependencies(services);
            var x = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DB.DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "NomNomNom",
                AutomaticChallenge = true,
                AutomaticAuthenticate = true,
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login")
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InjectDependencies(IServiceCollection services)
        {

            services.AddTransient<IConfigurationBuilder, ConfigurationBuilder>()
                .AddTransient<DB.DbContext>()
                .AddTransient<IGuestRepository, GuestRepostory>()
                .AddTransient<IGuestManager, GuestManager>()
                .AddTransient<IConfiguration, Configuration>();

        }
    }
}
