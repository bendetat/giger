using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chvart.Giger;
using Chvart.Raphael;
using Nancy;

namespace Chvart.TestSite
{
    public class Ellipses : NancyModule
    {
        public Ellipses()
        {
            Get["/examples/ellipses/1"] = _ =>
            {
                var svg = new Svg();

                svg
                    .Ellipse(200, 80, 100, 50)
                    .WithFill("yellow")
                    .WithStroke("purple")
                    .WithStrokeWidth(2);

                return svg.ToString();
            };

            Get["/examples/ellipses/2"] = _ =>
            {
                var svg = new Svg();

                svg.Ellipse(240, 100, 220, 30).WithFill("purple");
                svg.Ellipse(220, 70, 190, 20).WithFill("lime");
                svg.Ellipse(210, 45, 170, 15).WithFill("yellow");

                return svg.ToString();
            };

            Get["/examples/ellipses/3"] = _ =>
            {
                var svg = new Svg();

                svg.Ellipse(250, 50, 220, 30).WithFill("yellow");
                svg.Ellipse(220, 50, 190, 20).WithFill("white");

                return svg.ToString();
            };
        }
    }
}