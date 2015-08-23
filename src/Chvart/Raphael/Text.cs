using System.Xml;

namespace Chvart.Raphael
{
    public static partial class ChvartExtensions
    {
        public static Text Text(this Chvart chvart, int x, int y, string text)
        {
            var textElement = new Text(x, y, text);

            chvart.AddChild(textElement);

            return textElement;
        }
    }

    public class Text : Element
    {
        public Text(int x, int y, string text)
        {
            Attr(new
            {
                x,
                y,
                textAnchor = "middle",
                stroke = "none",
                fill = "#000000",
                font = "12px sans-serif"
            });

            AddChild(new TextSpan(text));

            AddStyle("-webkit-tap-highlight-color", "rgba(0,0,0,0)");
            AddStyle("text-anchor", "middle");
            AddStyle("font-style", "normal");
            AddStyle("font-variant", "normal");
            AddStyle("font-weight", "normal");
            AddStyle("font-stretch", "normal");
            AddStyle("font-size", "12px");
            AddStyle("font-style", "sans-serif");
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
                AddStyle("font", value.ToString());
            }
        }
    }
}