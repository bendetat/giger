using System.Xml;

namespace Chvart.Raphael
{
    public class TextSpan : Element
    {
        public TextSpan(string text)
        {
            Attr(new{
                dy = 4,
                style = "-webkit-tap-highlight-color: rgba(0,0,0,0);"
            });

            AddChild(new TextNode(text));
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("tspan");
        }
    }
}