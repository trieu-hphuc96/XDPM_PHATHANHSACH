using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XDPM_PHATHANHSACH.Startup))]
namespace XDPM_PHATHANHSACH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
