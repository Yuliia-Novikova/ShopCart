using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_shop_cart.Startup))]
namespace Online_shop_cart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
