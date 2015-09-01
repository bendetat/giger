using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Giger.Filters;
using Giger.Gradients;
using Giger.Plumbing;
using Giger.Shapes;
using Giger.Text;
using Humanizer;

namespace Giger
{
    public abstract class Element<T> : BaseElement
        where T : BaseElement
    {
        private readonly IDictionary<string, Attribute> _attributes = new Dictionary<string, Attribute>();
        private readonly IDictionary<string, string> _styles = new Dictionary<string, string>();

        protected Element() : base(null, null, null, null)
        {
        }

        protected Element(double? x, double? y, double? width, double? height)
            : base(x, y, width, height)
        {
            SetAttr(new {x, y, width, height});
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

            if (element is XmlElement && element.NamespaceURI != XmlDocumentExtensions.SvgNamespaceUri)
            {
                throw new InvalidOperationException($"The {element.Name} namespace should be \"{XmlDocumentExtensions.SvgNamespaceUri}\" but was \"{element.NamespaceURI}\"");
            }

            if (element.Attributes != null)
            {
                foreach (var attr in _attributes)
                {
                    if (attr.Key == "style")
                    {
                        continue;
                    }

                    var attribute = doc.CreateAttribute(attr.Key, attr.Value.Namespace);

                    attribute.Value = attr.Value.Value.ToString();

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
            foreach (var item in attributes.ToDictionary().Where(x => x.Value != null))
            {
                SetAttr(item.Key.Underscore().Hyphenate(), item.Value);
            }

            return this as T;
        }

        public T SetAttr(string name, object value, string @namespace = null)
        {
            if (name == "style")
            {
                this.WithStyleFromCss(value.ToString());
            }
            else
            {
                _attributes[name] = new Attribute
                {
                    Value = value,
                    Namespace = @namespace
                };
            }

            return this as T;
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
            return SetAttr(new {fill = ToRgb(red, green, blue)});
        }

        public T WithFill<TGradient>(Gradient<TGradient> gradient)
            where TGradient : Gradient<TGradient>
        {
            return SetAttr(new {fill = $"url(#{gradient.Id})"});
        }

        public T WithStrokeWidth(double strokeWidth)
        {
            return SetAttr(new {strokeWidth = strokeWidth});
        }

        public T WithStroke(int red, int green, int blue)
        {
            return SetAttr(new {stroke = ToRgb(red, green, blue)});
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

            return SetAttr(new {fillOpacity = opacity});
        }

        public T WithFill(string fill)
        {
            return SetAttr(new {fill = fill});
        }

        public T WithStroke(string stroke)
        {
            return SetAttr(new {stroke = stroke});
        }

        public T WithStrokeOpacity(double opacity)
        {
            ValidateOpacity(opacity);

            return SetAttr(new {strokeOpacity = opacity});
        }

        private static void ValidateOpacity(double opacity)
        {
            if (opacity < 0 || 1 < opacity) throw new ArgumentOutOfRangeException(nameof(opacity));
        }

        public T WithOpacity(double opacity)
        {
            ValidateOpacity(opacity);

            return SetAttr(new {opacity = opacity});
        }

        public T WithFillRule(FillRule fillRule)
        {
            return SetAttr(new {fillRule = fillRule.ToString().ToLower()});
        }

        public T WithFontFamily(string fontFamily)
        {
            return SetAttr(new
            {
                fontFamily
            });
        }

        public T WithTextAnchor(TextAnchor textAnchor)
        {
            return SetAttr(new
            {
                textAnchor = textAnchor.ToString().ToLower()
            });
        }

        public T WithFontSize(double fontSize)
        {
            return SetAttr(new
            {
                fontSize = fontSize
            });
        }

        public T WithFontSize(string fontSize)
        {
            return SetAttr(new {fontSize});
        }

        public T WithDx(double dx)
        {
            return SetAttr(new {dx});
        }

        public T WithDy(double dy)
        {
            return SetAttr(new {dy});
        }

        public T WithFontSizeAdjust(double fontSizeAdjust)
        {
            return SetAttr(new {fontSizeAdjust});
        }

        public T WithFontStretch(FontStretch fontStretch)
        {
            return SetAttr(new {fontStretch = fontStretch.ToString().ToLower()});
        }

        public T WithFontStyle(FontStyle fontStyle)
        {
            return SetAttr(new {fontStyle = fontStyle.ToString().ToLower()});
        }

        public T WithFontVariant(FontVariant fontVariant)
        {
            return SetAttr(new {fontVariant = fontVariant.ToString().ToLower()});
        }

        public T WithFontWeight(FontWeight fontWeight)
        {
            return SetAttr(new {fontWeight = fontWeight.ToString().ToLower()});
        }

        public T WithFontWeight(int fontWeight)
        {
            return SetAttr(new {fontWeight});
        }

        public T WithTransformRotate(double angle, double x = 0, double y = 0)
        {
            var existingTransform = GetAttrOrDefault("transform", "");

            return SetAttr(new
            {
                transform = $"{existingTransform} rotate({angle} {x}, {y})"
            });
        }

        protected string GetAttrOrDefault(string key, string @default)
        {
            return _attributes.ContainsKey(key) ? _attributes[key].Value.ToString() : @default;
        }

        public T WithStrokeLinecap(StrokeLinecap strokeLinecap)
        {
            return SetAttr(new
            {
                strokeLinecap = strokeLinecap.ToString().ToLower()
            });
        }

        public T WithStrokeDasharray(params double[] dashArray)
        {
            return SetAttr(new
            {
                strokeDasharray = string.Join(" ", dashArray.Select(x => x.ToString()))
            });
        }

        public T WithFilter(Filter filter)
        {
            return SetAttr("filter", $"url(#{filter.Id})");
        }

        public T WithWidth(string width)
        {
            return SetAttr(new {width});
        }

        public T WithHeight(string height)
        {
            return SetAttr(new {height});
        }

        private class Attribute
        {
            public object Value { get; set; }
            public string Namespace { get; set; }
        }
    }
}