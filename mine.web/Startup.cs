using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mine.web.Startup))]
namespace mine.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
