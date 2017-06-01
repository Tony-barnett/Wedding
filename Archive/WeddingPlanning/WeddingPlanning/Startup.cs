using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeddingPlanning.Startup))]
namespace WeddingPlanning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
