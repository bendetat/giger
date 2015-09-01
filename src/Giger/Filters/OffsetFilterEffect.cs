using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class OffsetFilterEffect : FilterEffect<OffsetFilterEffect>
    {
        public OffsetFilterEffect(double dx, double dy, string filterInputName, string resultName) : base(filterInputName, resultName)
        {
            SetAttr(new { dx, dy });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feOffset");
        }
    }
}