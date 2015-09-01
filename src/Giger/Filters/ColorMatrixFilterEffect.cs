using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class ColorMatrixFilterEffect : FilterEffect<ColorMatrixFilterEffect>
    {
        public ColorMatrixFilterEffect(double[] matrix, string filterInput, string resultName)
            : base(filterInput, resultName)
        {
            SetAttr("values", matrix.ToMatrix(4, 5).ToString());
        }

        public ColorMatrixFilterEffect(string matrix, string filterInput, string resultName)
            : base(filterInput, resultName)
        {
            SetAttr("values", matrix.ToMatrix(4, 5).ToString());
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feColorMatrix");
        }
    }
}