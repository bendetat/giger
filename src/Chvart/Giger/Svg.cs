using System.IO;
using System.Xml;
using Chvart.Giger;

namespace Chvart.Raphael
{
    public class Svg : Element<Svg>
    {
        public Svg() : base(0, 0, 0, 0)
        {
            SetAttr(new
            {
                height = 480,
                width = 640,
                version = "1.1",
                style = "overflow: hidden; position: relative;"
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("svg", "http://www.w3.org/2000/svg");
        }

        public override string ToString()
        {
            var doc = new XmlDocument();
            var xml = ToXml(doc);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                foreach (var node in xml)
                {
                    node.WriteTo(xmlWriter);
                }
                xmlWriter.Flush();

                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}