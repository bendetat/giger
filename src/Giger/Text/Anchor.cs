using System;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Text
{
    public class Anchor : Element<Anchor>
    {
        public Anchor(string href, string target = null)
        {
            SetAttr("xlink:href", href, "http://www.w3.org/1999/xlink");

            if (!string.IsNullOrEmpty(target))
            {
                SetAttr("target", target);
            }
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("a");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Anchor Anchor(this BaseElement baseElement, string href, string target = null)
        {
            var anchor = new Anchor(href, target);

            baseElement.AddChild(anchor);

            return anchor;
        }
    }
}