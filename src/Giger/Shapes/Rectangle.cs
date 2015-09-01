using System.Xml;
using Giger.Plumbing;

namespace Giger.Shapes
{
    public class Rectangle : Element<Rectangle>
    {
        public Rectangle(double x, double y, double width, double height) : base(x, y, width, height)
        {
            SetAttr(new
            {
                x, y, width, height
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("rect");
        }

        public Rectangle WithRoundedCorners(double rx, double ry)
        {
            WithStyle("rx", rx);
            WithStyle("ry", ry);

            return this;
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Rectangle Rectangle(this BaseElement baseElement, double x, double y, double width, double height)
        {
            var rectangle = new Rectangle(x, y, width, height);

            baseElement.AddChild(rectangle);

            return rectangle;
        }
    }
}