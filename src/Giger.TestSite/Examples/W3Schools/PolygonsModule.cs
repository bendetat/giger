using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_polygon.asp

    public class PolygonsModule : NancyModule
    {
        public PolygonsModule()
        {
            Get["/examples/w3schools/polygons/1"] = _ =>
            {
                var svg = new Svg(500,210);

                svg
                    .Polygon(200, 10, 250, 190, 160, 210)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(1);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/polygons/2"] = _ =>
            {
                var svg = new Svg(500,250);

                svg
                    .Polygon(220, 10, 300, 210, 170, 250, 123, 234)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(1);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/polygons/3"] = _ =>
            {
                var svg = new Svg(500,210);

                svg
                    .Polygon(100, 10, 40, 198, 190, 78, 10, 78, 160, 198)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(5)
                    .WithFillRule(FillRule.NonZero);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/polygons/4"] = _ =>
            {
                var svg = new Svg(500, 210);

                svg
                    .Polygon(100, 10, 40, 198, 190, 78, 10, 78, 160, 198)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(5)
                    .WithFillRule(FillRule.EvenOdd);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}