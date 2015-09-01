using System;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Gradients
{
    public class LinearGradient : Gradient<LinearGradient>
    {
        public LinearGradient(string x1, string y1, string x2, string y2)
        {
            SetAttr(new {x1, y1, x2, y2});
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("linearGradient");
        }
    }
}