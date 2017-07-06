using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Printajmo.Startup))]
namespace Printajmo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
