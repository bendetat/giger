using Giger.Text;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class TextModule : NancyModule
    {
        public TextModule()
        {
            Get["/examples/w3schools/text/1"] = _ =>
            {
                var svg = new Svg(200, 30);

                svg
                    .Text(0, 15, "I love SVG!")
                    .WithFill("red")
                    .WithFontSize(12);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/text/2"] = _ => {
                var svg = new Svg(200, 60);

                svg
                    .Text(0, 15, "I love SVG!")
                    .WithFill("red")
                    .WithFontSize(12)
                    .WithTransformRotate(30, 20, 40);


                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/text/3"] = _ => {
                var svg = new Svg(200, 90);

                var text = svg
                    .Text(10, 20, "Several lines:")
                    .WithFill("red")
                    .WithFontSize(12);
                text.AddSpan(10, 45, "First line.");
                text.AddSpan(10, 70, "Second line.");


                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/text/4"] = _ => {
                var svg = new Svg(200,30);

                var anchor = svg.Anchor("http://www.w3schools.com/svg", "_blank");
                anchor.Text(0, 15, "I love SVG!").WithFill("red");

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}