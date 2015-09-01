using System.Xml;
using Giger.Plumbing;

namespace Giger.Filters
{
    public class SpecularLightingFilterEffect : FilterEffect<SpecularLightingFilterEffect>
    {
        public SpecularLightingFilterEffect(
            double surfaceScale,
            double specularConstant,
            double specularExponent,
            string lightingColor,
            string filterInputName,
            string resultName
            ) : base(filterInputName, resultName)
        {
            SetAttr("surfaceScale", surfaceScale);
            SetAttr("specularConstant", specularConstant);
            SetAttr("specularExponent", specularExponent);
            SetAttr("lightingColor", lightingColor);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("feSpecularLighting");
        }

        public SpecularLightingFilterEffect AddPointLight(double x, double y, double z)
        {
            AddChild(new PointLight(x, y, z));

            return this;
        }
    }
}