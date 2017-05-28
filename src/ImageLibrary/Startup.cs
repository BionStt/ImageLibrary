using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageLibrary.Startup))]
namespace ImageLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
