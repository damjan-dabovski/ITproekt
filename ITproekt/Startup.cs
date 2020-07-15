using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITproekt.Startup))]
namespace ITproekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
