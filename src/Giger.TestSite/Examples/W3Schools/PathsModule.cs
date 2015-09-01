using Giger.Shapes;
using Giger.Text;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_path.asp

    public class PathsModule : NancyModule
    {
        public PathsModule()
        {
            Get["/examples/w3schools/paths/1"] = _ =>
            {
                var svg = new Svg(400, 210);

                svg.Path()
                    .MoveTo(150, 0)
                    .LineTo(75, 200)
                    .LineTo(225, 200)
                    .ClosePath();

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/paths/2"] = _ =>
            {
                var svg = new Svg(450, 400);

                svg.Path()
                    .MoveTo(100, 350)
                    .LineToRelative(150, -300)
                    .ClosePath()
                    .WithStroke("red")
                    .WithStrokeWidth(3)
                    .WithFill("none");
                svg.Path()
                    .MoveTo(250, 50)
                    .LineToRelative(150, 300)
                    .ClosePath()
                    .WithStroke("red")
                    .WithStrokeWidth(3)
                    .WithFill("none");
                svg.Path()
                    .MoveTo(175, 200)
                    .LineToRelative(150, 0)
                    .ClosePath()
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("none");

                svg.Path()
                    .MoveTo(100, 350)
                    .QuadraticBezierRelative(150, -300, 300, 000)
                    .WithStroke("blue")
                    .WithStrokeWidth(5)
                    .WithFill("none");

                // Mark relevant points
                var relevantPoints = svg.Group()
                    .WithStrokeWidth(3)
                    .WithFill("black");
                relevantPoints.Circle(100, 350, 3);
                relevantPoints.Circle(250, 50, 3);
                relevantPoints.Circle(400, 350, 3);
            
                // Label the points
                var labels = svg.Group()
                    .WithFontSize(30)
                    .WithFontFamily("sans-serif")
                    .WithFill("black")
                    .WithStroke("none")
                    .WithTextAnchor(TextAnchor.Middle);
                labels.Text(100, 350, "A").WithDx(-30);
                labels.Text(250, 50, "B").WithDy(-10);
                labels.Text(400, 350, "C").WithDx(30);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}