using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPA_BOOKING_SYSTEM.Startup))]
namespace SPA_BOOKING_SYSTEM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
