using Chvart.Giger;
using Chvart.Raphael;
using Nancy;

namespace Chvart.TestSite
{
    public class Lines : NancyModule
    {
        public Lines()
        {
            Get["/examples/lines/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Line(0, 0, 200, 200)
                    .WithStroke(255, 0, 0)
                    .WithStrokeWidth(2);

                return svg.ToString();
            };
        }
    }
}