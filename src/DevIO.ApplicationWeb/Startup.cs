using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevIO.ApplicationWeb.Startup))]
namespace DevIO.ApplicationWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
