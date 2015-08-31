using System.Xml;

namespace Giger
{
    public class Line : Element<Line>
    {
        public Line(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
            SetAttr(new {x1, y1, x2, y2});
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("line");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Line Line(this BaseElement baseElement, double x1, double y1, double x2, double y2)
        {
            var line = new Line(x1, y1, x2, y2);

            baseElement.AddChild(line);

            return line;
        }
    }
}