using System.Xml;
using Chvart.Raphael;

namespace Chvart.Giger
{
    public static partial class ChvartExtensions
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
            : base(x, y, 0, 0)
        {
            SetAttr(new
            {
                x,
                y,
                textAnchor = "middle",
                stroke = "none",
                fill = "#000000",
                font = "12px sans-serif"
            });

            AddChild(new TextSpan(text));

            WithStyle("text-anchor", "middle");
            WithStyle("font-style", "normal");
            WithStyle("font-variant", "normal");
            WithStyle("font-weight", "normal");
            WithStyle("font-stretch", "normal");
            WithStyle("font-size", "12px");
            WithStyle("font-style", "sans-serif");
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("text");
        }

        protected override void AddingAttribute(string key, object value)
        {
            if (key == "font")
            {
                RemoveStyles(x => x.StartsWith("font-"));
                WithStyle("font", value.ToString());
            }
        }
    }
}