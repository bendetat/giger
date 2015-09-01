using Giger.Filters;
using Giger.Nancy;
using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class DropShadowsModule : NancyModule
    {
        public DropShadowsModule()
        {
            Get["/examples/w3schools/drop-shadows/1"] = _ =>
            {
                var svg = new Svg(140, 140);

                var f1 = svg.Defs.AddFilter()
                    .WithWidth("200%")
                    .WithHeight("200%");
                f1.Offset(20, 20, FilterInput.SourceGraphic, "offOut");
                f1.Blend("offOut", FilterInput.SourceGraphic, BlendMode.Normal);

                svg
                    .Rectangle(0, 0, 90, 90)
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("yellow")
                    .WithFilter(f1);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/drop-shadows/2"] = _ =>
            {
                var svg = new Svg(140, 140);

                var f1 = svg.Defs.AddFilter()
                    .WithWidth("200%")
                    .WithHeight("200%");
                f1.Offset(20, 20, FilterInput.SourceGraphic, "offOut");
                f1.GaussianBlur(10, "offOut", "blurOut");
                f1.Blend(FilterInput.SourceGraphic, "blurOut", BlendMode.Normal);

                svg
                    .Rectangle(0, 0, 90, 90)
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("yellow")
                    .WithFilter(f1);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/drop-shadows/3"] = _ =>
            {
                var svg = new Svg(140, 140);

                var f1 = svg.Defs.AddFilter()
                    .WithWidth("200%")
                    .WithHeight("200%");
                f1.Offset(20, 20, FilterInput.SourceAlpha, "offOut");
                f1.GaussianBlur(10, "offOut", "blurOut");
                f1.Blend(FilterInput.SourceGraphic, "blurOut", BlendMode.Normal);

                svg
                    .Rectangle(0, 0, 90, 90)
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("yellow")
                    .WithFilter(f1);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/drop-shadows/4"] = _ =>
            {
                var svg = new Svg(140, 140);

                var f1 = svg.Defs.AddFilter()
                    .WithWidth("200%")
                    .WithHeight("200%");
                f1.Offset(20, 20, FilterInput.SourceGraphic, "offOut");
                f1.ColorMatrix(resultName: "matrixOut", filterInput: "offOut", matrix: @"
0.2,    0,      0,      0,  0,
0,      0.2,    0,      0,  0,
0,      0,      0.2,    0,  0,
0,      0,      0,      1,  0
");
                f1.GaussianBlur(resultName: "blurOut", filterInput: "matrixOut", standardDeviation: 10);
                f1.Blend(FilterInput.SourceGraphic, "blurOut", BlendMode.Normal);

                svg
                    .Rectangle(0, 0, 90, 90)
                    .WithStroke("green")
                    .WithStrokeWidth(3)
                    .WithFill("yellow")
                    .WithFilter(f1);

                return Response.AsSvg(svg);
            };
        }
    }
}