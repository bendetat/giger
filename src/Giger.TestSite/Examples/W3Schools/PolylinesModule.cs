using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_polyline.asp

    public class PolylinesModule : NancyModule
    {
        public PolylinesModule()
        {
            Get["/examples/w3schools/polylines/1"] = _ =>
            {
                var svg = new Svg(500,200);

                svg
                    .Polyline(20, 20, 40, 25, 60, 40, 80, 120, 120, 140, 200, 180)
                    .WithFill("none")
                    .WithStroke("black")
                    .WithStrokeWidth(3);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/polylines/2"] = _ =>
            {
                var svg = new Svg(500,180);

                svg
                    .Polyline(0, 40, 40, 40, 40, 80, 80, 80, 80, 120, 120, 120, 120, 160)
                    .WithFill("white")
                    .WithStroke("red")
                    .WithStrokeWidth(4);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}