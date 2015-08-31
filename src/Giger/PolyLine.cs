using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Giger
{
    public class PolyLine : Element<PolyLine>
    {
        public PolyLine(IEnumerable<double> points)
            : this(points.ToArray())
        {
        }

        public PolyLine(params double[] points)
            : base(0, 0, 0, 0)
        {
            SetPoints(points.ToPoints().ToArray());
        }

        public PolyLine(IEnumerable<Point> points)
            : this(points.ToArray())
        {
        }

        public PolyLine(params Point[] points) : base(0, 0, 0, 0)
        {
            SetPoints(points);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("polyline");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static PolyLine PolyLine(this BaseElement baseElement, params double[] points)
        {
            var polyline = new PolyLine(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static PolyLine PolyLine(this BaseElement baseElement, IEnumerable<double> points)
        {
            var polyline = new PolyLine(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static PolyLine PolyLine(this BaseElement baseElement, IEnumerable<Point> points)
        {
            var polyline = new PolyLine(points);

            baseElement.AddChild(polyline);

            return polyline;
        }

        public static PolyLine PolyLine(this BaseElement baseElement, params Point[] points)
        {
            var polyline = new PolyLine(points);

            baseElement.AddChild(polyline);

            return polyline;
        }
    }
}