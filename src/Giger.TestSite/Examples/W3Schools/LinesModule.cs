using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    // http://www.w3schools.com/svg/svg_line.asp

    public class LinesModule : NancyModule
    {
        public LinesModule()
        {
            Get["/examples/w3schools/lines/1"] = _ =>
            {
                var svg = new Svg(500,210);

                svg
                    .Line(0, 0, 200, 200)
                    .WithStroke(255, 0, 0)
                    .WithStrokeWidth(2);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}