using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class PointLight : Element<PointLight>
    {
        public PointLight(double x, double y, double z)
        {
            SetAttr(new {x, y, z});
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("fePointLight");
        }
    }
}