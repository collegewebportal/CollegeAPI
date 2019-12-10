using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CollegeService.Startup))]
namespace CollegeService
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
