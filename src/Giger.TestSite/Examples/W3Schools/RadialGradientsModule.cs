using Giger.Nancy;
using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class RadialGradientsModule : NancyModule
    {
        public RadialGradientsModule()
        {
            Get["/examples/w3schools/radial-gradients/1"] = _ =>
            {
                var svg = new Svg(500, 150);

                var grad1 = svg.Defs.AddRadialGradient("50%", "50%", "50%", "50%", "50%")
                    .AddStop("0%", "rgb(255,255,255)", 0)
                    .AddStop("100%", "rgb(0,0,255)", 1);

                svg.Ellipse(200, 70, 85, 55).WithFill(grad1);

                return Response.AsSvg(svg);
            };

            Get["/examples/w3schools/radial-gradients/2"] = _ =>
            {
                var svg = new Svg(500, 150);

                var grad2 = svg.Defs.AddRadialGradient("20%", "30%", "30%", "50%", "50%")
                    .AddStop("0%", "rgb(0,0,255)", 0)
                    .AddStop("100%", "rgb(0,0,255)", 1);

                svg.Ellipse(200, 70, 85, 55).WithFill(grad2);

                return Response.AsSvg(svg);
            };
        }
    }
}