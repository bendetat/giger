using System.Xml;

namespace Giger
{
    public class NoopElement : Element<NoopElement>
    {
        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return null;
        }
    }

    public static partial class BaseElementExtensions
    {
        public static BaseElement Noop(this BaseElement baseElement)
        {
            return new NoopElement();
        }
    }
}