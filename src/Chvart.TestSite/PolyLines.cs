using Chvart.Giger;
using Chvart.Raphael;
using Nancy;

namespace Chvart.TestSite
{
    public class PolyLines : NancyModule
    {
        public PolyLines()
        {
            Get["/examples/polylines/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .PolyLine(20, 20, 40, 25, 60, 40, 80, 120, 120, 140, 200, 180)
                    .WithFill("none")
                    .WithStroke("black")
                    .WithStrokeWidth(3);

                return svg.ToString();
            };

            Get["/examples/polylines/2"] = _ =>
            {
                var svg = new Svg();

                svg
                    .PolyLine(0, 40, 40, 40, 40, 80, 80, 80, 80, 120, 120, 120, 120, 160)
                    .WithFill("white")
                    .WithStroke("red")
                    .WithStrokeWidth(4);

                return svg.ToString();
            };
        }
    }
}