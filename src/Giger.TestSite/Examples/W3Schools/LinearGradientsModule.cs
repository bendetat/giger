using Giger.Nancy;
using Giger.Shapes;
using Giger.Text;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class LinearGradientsModule : NancyModule
    {
        public LinearGradientsModule()
        {
            Get["/examples/w3schools/linear-gradients/1"] = _ =>
            {
                var svg = new Svg(400, 150);

                var grad1 = svg.Defs
                    .AddLinearGradient("0%", "0%", "100%", "0%")
                    .AddStop("0%", "rgb(255,255,0)", 1)
                    .AddStop("100%", "rgb(255,0,0)", 1);

                svg.Ellipse(200, 70, 85, 55)
                    .WithFill(grad1);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/linear-gradients/2"] = _ =>
            {
                var svg = new Svg(400, 150);

                var grad2 = svg.Defs.AddLinearGradient("0%", "0%", "0%", "100%")
                    .AddStop("0%", "rgb(255,255,0)", 1)
                    .AddStop("100%", "rgb(255,0,0)", 1);

                svg.Ellipse(200, 70, 85, 55)
                    .WithFill(grad2);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/linear-gradients/3"] = _ =>
            {
                var svg = new Svg(400, 150);

                var grad1 = svg.Defs.AddLinearGradient("0%", "0%", "100%", "0%")
                    .AddStop("0%", "rgb(255,255,0)", 1)
                    .AddStop("100%", "rgb(255,0,0)", 1);

                svg.Ellipse(200, 70, 85, 55)
                    .WithFill(grad1);
                svg.Text(150, 86, "SVG")
                    .WithFill("#ffffff")
                    .WithFontSize(45)
                    .WithFontFamily("Verdana");

                return Response.AsSvg(svg);
            };
        }
    }
}