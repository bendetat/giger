using System.Xml;
using Giger.Plumbing;

namespace Giger.Gradients
{
    class GradientStop : Element<GradientStop>
    {
        public GradientStop(string offset, string stopColor, double stopOpacity = 1) : base(0,0,0,0)
        {
            SetAttr(new {offset, stopColor, stopOpacity});
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("stop");
        }
    }
}