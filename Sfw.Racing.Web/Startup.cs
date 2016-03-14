using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sfw.Racing.Web.Startup))]
namespace Sfw.Racing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
