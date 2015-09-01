using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_ellipse.asp

    public class EllipsesModule : NancyModule
    {
        public EllipsesModule()
        {
            Get["/examples/w3schools/ellipses/1"] = _ =>
            {
                var svg = new Svg(500, 140);

                svg
                    .Ellipse(200, 80, 100, 50)
                    .WithFill("yellow")
                    .WithStroke("purple")
                    .WithStrokeWidth(2);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/ellipses/2"] = _ =>
            {
                var svg = new Svg(500, 150);

                svg.Ellipse(240, 100, 220, 30).WithFill("purple");
                svg.Ellipse(220, 70, 190, 20).WithFill("lime");
                svg.Ellipse(210, 45, 170, 15).WithFill("yellow");

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/ellipses/3"] = _ =>
            {
                var svg = new Svg(500, 150);

                svg.Ellipse(250, 50, 220, 30).WithFill("yellow");
                svg.Ellipse(220, 50, 190, 20).WithFill("white");

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}