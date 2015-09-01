using Giger.Filters;
using Giger.Nancy;
using Giger.Shapes;
using Giger.Text;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class ExamplesModule : NancyModule
    {
        public ExamplesModule()
        {
            Get["/examples/w3schools/examples/1"] = _ =>
            {
                var svg = new Svg(120, 120);

                var filter = svg.Defs.AddFilter();
                filter.GaussianBlur(5);
                filter.Offset(5, 5);

                svg.Rectangle(0, 0, 90, 90).WithFill("grey").WithFilter(filter);
                svg.Rectangle(0, 0, 90, 90).WithFill("yellow").WithStroke("black");

                return Response.AsSvg(svg);
            };

            var decalSvg = new Svg(400, 200);
            decalSvg.Rectangle(1, 1, 198, 118).WithFill("#cccccc");
            var svgDecal = decalSvg.Group();
            svgDecal.Path("M50,90 C0,90 0,30 50,30 L150,30 C200,30 200,90 150,90 z")
                .WithFill("none")
                .WithStroke("#D90000")
                .WithStrokeWidth(10);
            svgDecal.Text(52, 76, "SVG")
                .WithFill("#FFFFFF")
                .WithStroke("black")
                .WithFontSize(45)
                .WithFontFamily("Verdana");

            Get["/examples/w3schools/examples/2"] = _ =>
            {
                var myFilter = decalSvg.Defs.AddFilter(0, 0, 200, 120)
                    .WithFilterUnits(FilterUnits.UserSpaceOnUse);
                myFilter.GaussianBlur(4, FilterInput.SourceAlpha, "blur");
                myFilter.Offset(4, 4, "blur", "offsetBlur");

                svgDecal.WithFilter(myFilter);

                return Response.AsSvg(decalSvg);
            };

            Get["/examples/w3schools/examples/3"] = _ =>
            {
                var myFilter = decalSvg.Defs.AddFilter(0, 0, 200, 120)
                    .WithFilterUnits(FilterUnits.UserSpaceOnUse);
                myFilter.GaussianBlur(4, FilterInput.SourceAlpha, "blur");
                myFilter.Offset(4, 4, "blur", "offsetBlur");
                myFilter.SpecularLighting(5, .75, 20, "#bbbbbb", "blur", "specOut")
                    .AddPointLight(-5000, -10000, 20000);

                svgDecal.WithFilter(myFilter);

                return Response.AsSvg(decalSvg);
            };

            Get["/examples/w3schools/examples/4"] = _ =>
            {
                var myFilter = decalSvg.Defs.AddFilter(0, 0, 200, 120)
                    .WithFilterUnits(FilterUnits.UserSpaceOnUse);
                myFilter.GaussianBlur(4, FilterInput.SourceAlpha, "blur");
                myFilter.Offset(4, 4, "blur", "offsetBlur");
                myFilter.SpecularLighting(5, .75, 20, "#bbbbbb", "blur", "specOut")
                    .AddPointLight(-5000, -10000, 20000);
                myFilter.Composite("specOut", FilterInput.SourceAlpha, CompositeOperator.In, "specOut");

                svgDecal.WithFilter(myFilter);

                return Response.AsSvg(decalSvg);
            };
        }
    }
}