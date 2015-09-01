using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_circle.asp

    public class CirclesModule : NancyModule
    {
        public CirclesModule()
        {
            Get["/examples/w3schools/circles/1"] = _ =>
            {
                var svg = new Svg(100, 100);

                svg
                    .Circle(50, 50, 40)
                    .WithStroke("black")
                    .WithStrokeWidth(3)
                    .WithFill("red");

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}