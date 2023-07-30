using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevIO.ApplicationMVC.Startup))]
namespace DevIO.ApplicationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
