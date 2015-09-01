using System.Xml;
using Giger.Plumbing;

namespace Giger.Text
{
    public class TextSpan : Element<TextSpan>
    {
        public TextSpan(string text)
        {
            AddChild(new TextNode(text));
        }

        public TextSpan(double x, double y, string text) : base(x, y, null, null)
        {
            AddChild(new TextNode(text));
            SetAttr(new {x, y});
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("tspan");
        }
    }
}