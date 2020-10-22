using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DcssePortal.Web.Startup))]
namespace DcssePortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
