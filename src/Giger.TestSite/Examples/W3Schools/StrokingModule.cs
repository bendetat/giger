using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giger.Shapes;
using Nancy;

namespace Giger.TestSite.Examples.W3Schools
{
    public class StrokingModule :  NancyModule
    {
        public StrokingModule()
        {
            Get["/examples/w3schools/stroking/1"] = _ =>
            {
                var svg = new Svg(300, 80);

                var group = svg
                    .Group()
                    .WithFill("none");
                group
                    .Path()
                    .WithStroke("red")
                    .MoveTo(5, 20)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStroke("blue")
                    .MoveTo(5, 40)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStroke("black")
                    .MoveTo(5, 60)
                    .LineToRelative(215, 0);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/stroking/2"] = _ =>
            {
                var svg = new Svg(300, 80);

                var group = svg
                    .Group()
                    .WithFill("none")
                    .WithStroke("black");
                group
                    .Path()
                    .WithStrokeWidth(2)
                    .MoveTo(5, 20)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeWidth(4)
                    .MoveTo(5, 40)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeWidth(6)
                    .MoveTo(5, 60)
                    .LineToRelative(215, 0);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/stroking/3"] = _ =>
            {
                var svg = new Svg(300, 80);

                var group = svg
                    .Group()
                    .WithFill("none")
                    .WithStroke("black")
                    .WithStrokeWidth(6);
                group
                    .Path()
                    .WithStrokeLinecap(StrokeLinecap.Butt)
                    .MoveTo(5, 20)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeLinecap(StrokeLinecap.Round)
                    .MoveTo(5, 40)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeLinecap(StrokeLinecap.Square)
                    .MoveTo(5, 60)
                    .LineToRelative(215, 0);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };

            Get["/examples/w3schools/stroking/4"] = _ =>
            {
                var svg = new Svg(300, 80);

                var group = svg
                    .Group()
                    .WithFill("none")
                    .WithStroke("black")
                    .WithStrokeWidth(4);
                group
                    .Path()
                    .WithStrokeDasharray(5, 5)
                    .MoveTo(5, 20)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeDasharray(10,10)
                    .MoveTo(5, 40)
                    .LineToRelative(215, 0);
                group
                    .Path()
                    .WithStrokeDasharray(20,10,5,5,5,10)
                    .MoveTo(5, 60)
                    .LineToRelative(215, 0);

                return Response.AsText(svg.ToString(), "image/svg+xml");
            };
        }
    }
}