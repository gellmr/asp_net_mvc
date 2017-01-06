using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(asp_net_mvc.Startup))]
namespace asp_net_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
