using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DMS_Website.Startup))]
namespace DMS_Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
