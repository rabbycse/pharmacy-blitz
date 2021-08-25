using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pharmacy.Web.Startup))]
namespace Pharmacy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
