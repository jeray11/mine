using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mine.test.web.Startup))]
namespace mine.test.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
