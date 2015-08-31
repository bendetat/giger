using System.Xml;

namespace Giger
{
    public class TextNode : Element<TextNode>
    {
        private readonly string _text;

        public TextNode(string text) : base(0,0,0,0)
        {
            _text = text;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateTextNode(_text);
        }
    }
}