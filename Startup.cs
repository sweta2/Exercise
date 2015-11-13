using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(customer_application.Startup))]
namespace customer_application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
