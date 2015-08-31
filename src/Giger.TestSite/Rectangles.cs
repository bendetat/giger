using Nancy;

namespace Giger.TestSite
{
    public class Rectangles : NancyModule
    {
        public Rectangles()
        {
            Get["/examples/rectangles/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Rectangle(0, 0, 300, 100)
                    .WithFill(0, 0, 255)
                    .WithStrokeWidth(3)
                    .WithStroke(0, 0, 0);

                return svg.ToString();
            };

            Get["/examples/rectangles/2"] = _ =>
            {
                var svg = new Svg();

                svg.Rectangle(50, 20, 150, 150)
                    .WithFill("blue")
                    .WithStroke("pink")
                    .WithStrokeWidth(5)
                    .WithFillOpacity(0.1)
                    .WithStrokeOpacity(0.9);

                return svg.ToString();
            };

            Get["/examples/rectangles/3"] = _ =>
            {
                var svg = new Svg();

                svg.Rectangle(50, 20, 150, 150)
                    .WithFill("blue")
                    .WithStroke("pink")
                    .WithStrokeWidth(5)
                    .WithOpacity(0.5);

                return svg.ToString();
            };

            Get["/examples/rectangles/4"] = _ =>
            {
                var svg = new Svg();

                svg.Rectangle(50, 20, 150, 150)
                    .WithRoundedCorners(20, 20)
                    .WithFill("red")
                    .WithStroke("black")
                    .WithStrokeWidth(5)
                    .WithOpacity(0.5);

                return svg.ToString();
            };
        }
    }
}