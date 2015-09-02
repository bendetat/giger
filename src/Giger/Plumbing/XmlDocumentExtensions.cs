using System.Xml;

namespace Giger.Plumbing
{
    public static class XmlDocumentExtensions
    {
        public static string SvgNamespaceUri = "http://www.w3.org/2000/svg";

        public static XmlElement CreateSvgElement(this XmlDocument doc, string qualifiedName)
        {
            return doc.CreateElement(qualifiedName, SvgNamespaceUri);
        }
    }
}