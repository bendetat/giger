using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class GaussianBlurFilterEffect : FilterEffect<GaussianBlurFilterEffect>
    {
        public GaussianBlurFilterEffect(double standardDeviationX, double standardDeviationY, string filterInputName, string resultName)
            : base(filterInputName, resultName)
        {
            SetAttr("stdDeviation", $"{standardDeviationX} {standardDeviationY}");
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feGaussianBlur");
        }
    }
}