using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eStoreWebsite.Startup))]
namespace eStoreWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
