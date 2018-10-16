using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CyrusCorp.SEO.Analyser.Web.Startup))]
namespace CyrusCorp.SEO.Analyser.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
