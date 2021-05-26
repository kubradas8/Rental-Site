using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentalCRUD.Startup))]
namespace RentalCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
