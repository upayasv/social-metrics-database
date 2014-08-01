using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UpayaWebApp.Startup))]
namespace UpayaWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
