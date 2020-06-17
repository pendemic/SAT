using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAT.UI.MVC.Startup))]
namespace SAT.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
