using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VFM.Web.Startup))]
namespace VFM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
