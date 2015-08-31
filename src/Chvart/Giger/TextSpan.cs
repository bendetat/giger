using System.Xml;

namespace Chvart.Giger
{
    public class TextSpan : Element<TextSpan>
    {
        public TextSpan(string text) : base(0,0,0,0)
        {
            SetAttr(new{
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