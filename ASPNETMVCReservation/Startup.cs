using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPNETMVCReservation.Startup))]
namespace ASPNETMVCReservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
