using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Chvart.Plumbing;
using Humanizer;

namespace Chvart
{
    public abstract class Element
    {
        private readonly IList<Element> _children = new List<Element>();
        private readonly IDictionary<string,object> _attributes = new Dictionary<string, object>();
        private readonly IDictionary<string,string> _styles = new Dictionary<string, string>();

        protected abstract XmlNode GetXmlNode(XmlDocument doc);

        public void AddChild(Element element)
        {
            _children.Add(element);
        }

        public XmlNode ToXml(XmlDocument doc)
        {
            var element = GetXmlNode(doc);

            if (element == null)
            {
                throw new InvalidOperationException("Element must be translatable to an XML element");
            }

            if (element.Attributes != null)
            {
                foreach (var attr in _attributes)
                {
                    var attribute = doc.CreateAttribute(attr.Key.Underscore().Hyphenate());

                    attribute.Value = attr.Value.ToString();

                    element.Attributes.Append(attribute);
                }

                if (!_attributes.ContainsKey("style") && _styles.Any())
                {
                    var attribute = doc.CreateAttribute("style");

                    attribute.Value = string.Join(";", _styles.Select(x => $"{x.Key}: {x.Value}"));

                    element.Attributes.Append(attribute);
                }
            }

            foreach (var child in _children)
            {
                element.AppendChild(child.ToXml(doc));
            }

            return element;
        }

        public void Attr(object attributes)
        {
            foreach (var item in attributes.ToDictionary())
            {
                _attributes[item.Key] = item.Value;
                AddingAttribute(item.Key, item.Value);
            }
        }

        protected void AddStyle(string key, string style)
        {
            _styles[key] = style;
        }

        protected void RemoveStyles(Func<string, bool> where)
        {
            foreach (var style in _styles.Where(x => where(x.Key)).ToArray())
            {
                _styles.Remove(style);
            }
        }

        protected virtual void AddingAttribute(string key, object value) { }
    }
}