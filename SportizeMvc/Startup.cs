using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportizeMvc.Startup))]
namespace SportizeMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
