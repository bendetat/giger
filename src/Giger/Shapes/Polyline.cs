using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Shapes
{
    public class Polyline : Element<Polyline>
    {
        public Polyline(IEnumerable<double> points)
            : this(points.ToArray())
        {
        }

        public Polyline(params double[] points)
        {
            SetPoints(points.ToPoints().ToArray());
        }

        public Polyline(IEnumerable<Point> points)
            : this(points.ToArray())
        {
        }

        public Polyline(params Point[] points)
        {
            SetPoints(points);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("polyline");
        }

        public Polyline SetPoints(Point[] points)
        {
            SetX(points.Min(x => x.X));
            SetY(points.Min(x => x.Y));
            SetWidth(points.Max(x => x.X) - this.X ?? 0);
            SetHeight(points.Max(x => x.Y) - this.Y ?? 0);

            return SetAttr(new
            {
                points = string.Join(" ", points.Select(x => $"{x.X},{x.Y}"))
            });
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Polyline Polyline(this BaseElement baseElement, params double[] points)
        {
            var polyline = new Polyline(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static Polyline Polyline(this BaseElement baseElement, IEnumerable<double> points)
        {
            var polyline = new Polyline(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static Polyline Polyline(this BaseElement baseElement, IEnumerable<Point> points)
        {
            var polyline = new Polyline(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static Polyline Polyline(this BaseElement baseElement, params Point[] points)
        {
            var polyline = new Polyline(points);

            baseElement.AddChild(polyline);

            return polyline;
        }
    }
}