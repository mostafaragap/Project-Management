using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectManag.Startup))]
namespace ProjectManag
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
