using Nancy;

namespace Giger.TestSite
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