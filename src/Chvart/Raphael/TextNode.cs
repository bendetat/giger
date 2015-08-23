using System.Xml;

namespace Chvart.Raphael
{
    public class TextNode : Element
    {
        private readonly string _text;

        public TextNode(string text)
        {
            _text = text;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateTextNode(_text);
        }
    }
}