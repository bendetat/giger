using System.Xml;

namespace Giger.Text
{
    public class TextNode : Element<TextNode>
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