using System.Xml;
using Giger.Plumbing;

namespace Giger
{
    public class Group : Element<Group>
    {
        public Group() : base(null,null,null,null)
        {
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("g");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Group Group(this BaseElement baseElement)
        {
            var group = new Group();

            baseElement.AddChild(group);

            return group;
        }
    }
}