using Nancy;
using Nancy.Conventions;

namespace Giger.TestSite
{
    class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            Conventions.StaticContentsConventions.Clear();
            Conventions.StaticContentsConventions.AddDirectory("/", "site");

            base.ConfigureConventions(nancyConventions);
        }
    }
}