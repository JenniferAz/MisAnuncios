using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MisAnuncios.Startup))]
namespace MisAnuncios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
