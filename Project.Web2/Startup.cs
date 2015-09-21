using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project.Web2.Startup))]
namespace Project.Web2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
