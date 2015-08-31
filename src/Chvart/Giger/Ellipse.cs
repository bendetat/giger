using System.Xml;

namespace Chvart.Giger
{
    public class Ellipse : Element<Ellipse>
    {
        public Ellipse(double x, double y, double radiusX, double radiusY) : base(x - radiusX, y - radiusY, 2*radiusX, 2*radiusY)
        {
            SetAttr(new
            {
                cx = x,
                cy = y,
                rx = radiusX,
                ry = radiusY
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("ellipse");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Ellipse Ellipse(this BaseElement baseElement, double x, double y, double radiusX, double radiusY)
        {
            var ellipse = new Ellipse(x, y, radiusX, radiusY);

            baseElement.AddChild(ellipse);

            return ellipse;
        }
    }
}