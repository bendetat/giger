using System.Xml;
using Giger.Plumbing;
using Humanizer;

namespace Giger.Filters
{
    public class CompositeFilterEffect : FilterEffect<CompositeFilterEffect>
    {
        public CompositeFilterEffect(string filterInput1, string filterInput2, CompositeOperator @operator, string resultName) : base(filterInput1, resultName)
        {
            SetAttr(new
            {
                in1 = filterInput1,
                in2 = filterInput2,
            });
            SetAttr("operator", @operator.ToString().Camelize());
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feComposite");
        }
    }
}