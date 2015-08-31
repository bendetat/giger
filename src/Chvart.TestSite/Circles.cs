using Chvart.Giger;
using Chvart.Raphael;
using Nancy;

namespace Chvart.TestSite
{
    public class Circles : NancyModule
    {
        public Circles()
        {
            Get["/examples/circles/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Circle(50, 50, 40)
                    .WithStroke("black")
                    .WithStrokeWidth(3)
                    .WithFill("red");

                return svg.ToString();
            };
        }
    }
}