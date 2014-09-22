using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tcbcsl.Presentation.Startup))]
namespace Tcbcsl.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
