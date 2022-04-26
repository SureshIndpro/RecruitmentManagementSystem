using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test_Identity.Startup))]
namespace Test_Identity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
