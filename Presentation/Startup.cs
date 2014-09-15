using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Presentation.Startup))]
namespace Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
