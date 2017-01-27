using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gellmvc.Startup))]
namespace gellmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
