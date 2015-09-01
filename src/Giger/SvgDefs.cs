using System.Xml;
using Giger.Filters;
using Giger.Gradients;
using Giger.Plumbing;

namespace Giger
{
    public class SvgDefs : Element<SvgDefs>
    {
        public Filter AddFilter(double? x = null, double? y = null, double? width = null, double? height = null)
        {
            var filter = new Filter(x, y, width, height);

            AddChild(filter);

            return filter;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("defs");
        }

        public LinearGradient AddLinearGradient(string x1, string y1, string x2, string y2)
        {
            var gradient = new LinearGradient(x1, y1, x2, y2);

            AddChild(gradient);

            return gradient;
        }

        public RadialGradient AddRadialGradient(string centerX, string centerY, string radius, string focalX = null, string focalY = null)
        {
            var gradient = new RadialGradient(centerX, centerY, radius, focalX, focalY);

            AddChild(gradient);

            return gradient;
        }
    }
}