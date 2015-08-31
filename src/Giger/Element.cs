using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Giger.Plumbing;
using Humanizer;

namespace Giger
{
    public abstract class Element<T> : BaseElement
        where T: BaseElement
    {
        private readonly IDictionary<string, object> _attributes = new Dictionary<string, object>();
        private readonly IDictionary<string, string> _styles = new Dictionary<string, string>();

        protected Element(double x, double y, double width, double height)
            : base(x, y, width, height)
        {
        }

        protected abstract XmlNode GetXmlNode(XmlDocument doc);

        public override IEnumerable<XmlNode> ToXml(XmlDocument doc)
        {
            if (Removed)
            {
                yield break;
            }

            var element = GetXmlNode(doc);

            if (element == null)
            {
                throw new InvalidOperationException("Element must be translatable to an XML element");
            }

            if (element.Attributes != null)
            {
                foreach (var attr in _attributes)
                {
                    if (attr.Key == "style")
                    {
                        continue;
                    }

                    var attribute = doc.CreateAttribute(attr.Key.Underscore().Hyphenate());

                    attribute.Value = attr.Value.ToString();

                    element.Attributes.Append(attribute);
                }

                if (_styles.Any())
                {
                    var attribute = doc.CreateAttribute("style");

                    attribute.Value = string.Join(";", _styles.Select(x => $"{x.Key}: {x.Value}"));

                    element.Attributes.Append(attribute);
                }
            }

            foreach (var child in Children.SelectMany(x => x.ToXml(doc)))
            {
                element.AppendChild(child);
            }

            yield return element;
        }

        public T SetAttr(object attributes)
        {
            foreach (var item in attributes.ToDictionary())
            {
                if (item.Key == "style")
                {
                    this.WithStyleFromCss(item.Value.ToString());
                }
                else
                {
                    _attributes[item.Key] = item.Value;
                    AddingAttribute(item.Key, item.Value);
                }
            }

            return this as T;
        }

        protected virtual void AddingAttribute(string key, object value)
        {
        }

        protected T RemoveStyles(Func<string, bool> where)
        {
            foreach (var style in _styles.Where(x => where(x.Key)).ToArray())
            {
                _styles.Remove(style.Key);
            }

            return this as T;
        }

        protected T WithStyle(string key, object style)
        {
            _styles[key] = style.ToString();

            return this as T;
        }

        public T WithStyleFromCss(string css)
        {
            var styles = from command in css.Split(';')
                where command.Contains(":")
                let key = command.Substring(0, command.IndexOf(":"))
                let value = command.Substring(command.IndexOf(":") + 1)
                select new
                {
                    key,
                    value
                };
            foreach (var kvp in styles)
            {
                WithStyle(kvp.key, kvp.value);
            }

            return this as T;
        }

        public T WithFill(int red, int green, int blue)
        {
            return WithStyle("fill", ToRgb(red, green, blue));
        }

        public T WithStrokeWidth(double strokeWidth)
        {
            return this.WithStyle("stroke-width", strokeWidth);
        }

        public T WithStroke(int red, int green, int blue)
        {
            return this.WithStyle("stroke", ToRgb(red, green, blue));
        }

        private static string ToRgb(int red, int green, int blue)
        {
            if (red < 0 || 255 < red) throw new ArgumentOutOfRangeException(nameof(red));
            if (green < 0 || 255 < green) throw new ArgumentOutOfRangeException(nameof(green));
            if (blue < 0 || 255 < blue) throw new ArgumentOutOfRangeException(nameof(blue));

            return $"rgb({red}, {green}, {blue})";
        }

        public T WithFillOpacity(double opacity)
        {
            ValidateOpacity(opacity);

            return WithStyle("fill-opacity", opacity);
        }

        public T WithFill(string fill)
        {
            return WithStyle("fill", fill);
        }

        public T WithStroke(string stroke)
        {
            return WithStyle("stroke", stroke);
        }

        public T WithStrokeOpacity(double opacity)
        {
            ValidateOpacity(opacity);

            return WithStyle("stroke-opacity", opacity);
        }

        static void ValidateOpacity(double opacity)
        {
            if (opacity < 0 || 1 < opacity) throw new ArgumentOutOfRangeException(nameof(opacity));
        }

        public T WithOpacity(double opacity)
        {
            ValidateOpacity(opacity);

            return WithStyle("opacity", opacity);
        }

        public T WithFillRule(FillRule fillRule)
        {
            return WithStyle("fill-rule", fillRule.ToString().ToLower());
        }

        public T SetPoints(Point[] points)
        {
            SetX(points.Min(x => x.X));
            SetY(points.Min(x => x.Y));
            SetWidth(points.Max(x => x.X) - this.X);
            SetHeight(points.Max(x => x.Y) - this.Y);

            return SetAttr(new
            {
                points = string.Join(" ", points.Select(x => $"{x.X},{x.Y}"))
            });
        }
    }
}