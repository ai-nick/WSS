using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WilliamsWeb1.Startup))]
namespace WilliamsWeb1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
