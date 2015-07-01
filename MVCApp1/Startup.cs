using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCApp1.Startup))]
namespace MVCApp1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
