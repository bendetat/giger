using System.IO;
using System.Xml;

namespace Chvart
{
    public class Chvart : Element
    {
        public Chvart()
        {
            Attr(new
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

        public string ToSvg()
        {
            var doc = new XmlDocument();
            var xml = ToXml(doc);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                xml.WriteTo(xmlWriter);
                xmlWriter.Flush();

                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}