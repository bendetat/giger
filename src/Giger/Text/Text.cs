using System.Xml;
using Giger.Plumbing;

namespace Giger.Text
{
    public static partial class BaseElementExtensions
    {
        public static Text Text(this BaseElement element, double x, double y, string text)
        {
            var textElement = new Text(x, y, text);

            element.AddChild(textElement);

            return textElement;
        }
    }

    public class Text : Element<Text>
    {
        public Text(double x, double y, string text)
            : base(x, y, null, null)
        {
            AddChild(new TextSpan(text));
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("text");
        }

        public TextSpan AddSpan(double x, double y, string text)
        {
            var span = new TextSpan(x, y, text);

            AddChild(span);

            return span;
        }
    }
}