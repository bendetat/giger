using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof (Giger.TestSite.Startup))]

namespace Giger.TestSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseNancy(new NancyOptions
                {
                    Bootstrapper = new Bootstrapper()
                })
                .UseStageMarker(PipelineStage.MapHandler);
        }
    }
}