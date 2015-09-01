using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_rect.asp

    public class RectanglesModule : NancyModule
    {
        public RectanglesModule()
        {
            Get["/examples/w3schools/rectangles/1"] = _ =>
            {
                var svg = new Svg(400, 110);

                svg
                    .Rectangle(0, 0, 300, 100)
                    .WithFill(0, 0, 255)
                    .WithStrokeWidth(3)
                    .WithStroke(0, 0, 0);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/rectangles/2"] = _ =>
            {
                var svg = new Svg(400, 180);

                svg.Rectangle(50, 20, 150, 150)
                    .WithFill("blue")
                    .WithStroke("pink")
                    .WithStrokeWidth(5)
                    .WithFillOpacity(0.1)
                    .WithStrokeOpacity(0.9);

                return Response.AsText(svg.ToString()).WithContentType("image/svg+xml");
            };

            Get["/examples/w3schools/rectangles/3"] = _ =>
            {
                var svg = new Svg(400, 180);

                svg.Rectangle(50, 20, 150, 150)
                    .WithFill("blue")
                    .WithStroke("pink")
                    .WithStrokeWidth(5)
                    .WithOpacity(0.5);

                return Response.AsText(svg.ToString()).WithContentType("image/svg+xml");
            };

            Get["/examples/w3schools/rectangles/4"] = _ =>
            {
                var svg = new Svg(400, 180);

                svg.Rectangle(50, 20, 150, 150)
                    .WithRoundedCorners(20, 20)
                    .WithFill("red")
                    .WithStroke("black")
                    .WithStrokeWidth(5)
                    .WithOpacity(0.5);

                return Response.AsText(svg.ToString()).WithContentType("image/svg+xml");
            };
        }
    }
}