using Giger.Filters;
using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class BlurEffectsModule : NancyModule
    {
        public BlurEffectsModule()
        {
            Get["/examples/w3schools/blur-effects/1"] = _ =>
            {
                var svg = new Svg(110, 110);

                var f1 = svg.Defs.AddFilter();
                f1.GaussianBlur(15, FilterInput.SourceGraphic);

                svg
                    .Rectangle(0, 0, 90, 90)
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("yellow")
                    .WithFilter(f1);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}