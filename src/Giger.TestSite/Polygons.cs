using Nancy;

namespace Giger.TestSite
{
    public class Polygons : NancyModule
    {
        public Polygons()
        {
            Get["/examples/polygons/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Polygon(200, 10, 250, 190, 160, 210)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(1);

                return svg.ToString();
            };

            Get["/examples/polygons/2"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Polygon(220, 10, 300, 210, 170, 250, 123, 234)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(1);

                return svg.ToString();
            };

            Get["/examples/polygons/3"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Polygon(100, 10, 40, 198, 190, 78, 10, 78, 160, 198)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(5)
                    .WithFillRule(FillRule.NonZero);

                return svg.ToString();
            };

            Get["/examples/polygons/4"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Polygon(100, 10, 40, 198, 190, 78, 10, 78, 160, 198)
                    .WithFill("lime")
                    .WithStroke("purple")
                    .WithStrokeWidth(5)
                    .WithFillRule(FillRule.EvenOdd);

                return svg.ToString();
            };
        }
    }
}