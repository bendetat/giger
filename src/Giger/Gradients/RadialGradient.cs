using System.Xml;
using Giger.Plumbing;

namespace Giger.Gradients
{
    public class RadialGradient : Gradient<RadialGradient>
    {
        public RadialGradient( string centerX, string centerY, string radius, string focalX = null, string focalY = null)
        {
            SetAttr(new
            {
                cx = centerX,
                cy = centerY,
                r = radius
            });

            if (!string.IsNullOrEmpty(focalX))
            {
                SetAttr(new {fx = focalX});
            }

            if (!string.IsNullOrEmpty(focalY))
            {
                SetAttr(new {fy = focalY});
            }
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("radialGradient");
        }
    }
}