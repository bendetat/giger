using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class BlendFilterEffect : FilterEffect<BlendFilterEffect>
    {
        public BlendFilterEffect(string filterInputName, string filterInput2Name, BlendMode mode, string resultName) : base(filterInputName, resultName)
        {
            SetAttr(new
            {
                in2 = filterInput2Name,
                mode
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feBlend");
        }
    }
}