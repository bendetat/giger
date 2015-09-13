using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Giger.Filters;
using Giger.Plumbing;
using Giger.Shapes;

namespace Giger
{
    public class Svg : Element<Svg>, IManualDraw<Svg>
    {
        private string _backgroundFill;

        public Svg(double width = 640, double height = 480) : base(null, null, width, height)
        {
            Defs = new SvgDefs();
            AddChild(Defs);
        }

        public SvgDefs Defs { get; }

        public Svg Draw()
        {
            if (!string.IsNullOrWhiteSpace(_backgroundFill))
            {
                this.Rectangle(0, 0, (this.Width ?? 0), (this.Height ?? 0))
                    .WithFill(_backgroundFill);
            }

            return this;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("svg");
        }

        /// <summary>
        /// Returns a string containing the SVG XML, including a DOCTYPE and XML declaration
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(inline: false);
        }

        /// <summary>
        /// Returns a string containing the SVG XML, without a DOCTYPE or XML declaration
        /// for use inline.
        /// </summary>
        /// <returns></returns>
        public string ToInlineSvgString()
        {
            return ToString(inline: true);
        }

        private string ToString(bool inline)
        {
            var doc = new XmlDocument();
            var xml = ToXml(doc);

            if (inline) return xml.First().OuterXml;

            var writerSettings = new XmlWriterSettings()
            {
                ConformanceLevel = ConformanceLevel.Document,
                Indent = true
            };

            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            using (var xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                xmlWriter.WriteDocType("svg", "-//W3C/DTD SVG 1.1//EN", "http://www.w3c.org/Graphics/SVG/1.1/DTD/svg11.dtd", null);

                foreach (var node in xml)
                {
                    node.WriteTo(xmlWriter);
                }

                xmlWriter.Flush();

                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public static Circle MakeCircle(double x, double y, double radius)
        {
            return new Circle(x, y, radius);
        }

        public static Ellipse MakeEllipse(double x, double y, double radiusX, double radiusY)
        {
            return new Ellipse(x, y, radiusX, radiusY);
        }

        public static Line MakeLine(double x1, double y1, double x2, double y2)
        {
            return new Line(x1, y1, x2, y2);
        }

        public static Shapes.Path MakePath()
        {
            return new Shapes.Path();
        }

        public static Shapes.Path MakePath(string commandString)
        {
            return new Shapes.Path(commandString);
        }

        public static Polygon MakePolygon(params double[] points)
        {
            return new Polygon(points);
        }

        public static Polygon MakePolygon(IEnumerable<double> points)
        {
            return new Polygon(points);
        }

        public static Polygon MakePolygon(IEnumerable<Point> points)
        {
            return new Polygon(points);
        }

        public static Polygon MakePolygon(params Point[] points)
        {
            return new Polygon(points);
        }

        public static Polyline MakePolyline(params double[] points)
        {
            return new Polyline(points);
        }

        public static Polyline MakePolyline(IEnumerable<double> points)
        {
            return new Polyline(points);
        }

        public static Polyline MakePolyline(IEnumerable<Point> points)
        {
            return new Polyline(points);
        }

        public static Polyline MakePolyline(params Point[] points)
        {
            return new Polyline(points);
        }

        public static Rectangle MakeRectangle(double x, double y, double width, double height)
        {
            return new Rectangle(x, y, width, height);
        }

        public static Text.Text MakeText(double x, double y, string text)
        {
            return new Text.Text(x, y, text);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviation)
        {
            return new GaussianBlurFilterEffect(standardDeviation, standardDeviation, null, null);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviationX, double standardDeviationY)
        {
            return new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, null, null);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviation, FilterInput filterInput, string resultName = null)
        {
            return new GaussianBlurFilterEffect(standardDeviation, standardDeviation, filterInput.ToString(), resultName);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviationX, double standardDeviationY, FilterInput filterInput, string resultName = null)
        {
            return new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, filterInput.ToString(), resultName);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviation, string filterInput, string resultName = null)
        {
            return new GaussianBlurFilterEffect(standardDeviation, standardDeviation, filterInput, resultName);
        }

        public static GaussianBlurFilterEffect MakeGaussianBlurFilterEffect(double standardDeviationX, double standardDeviationY, string filterInput, string resultName = null)
        {
            return new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, filterInput, resultName);
        }

        public static OffsetFilterEffect MakeOffsetFilterEffect(double dx, double dy)
        {
            return new OffsetFilterEffect(dx, dy, null, null);
        }

        public static OffsetFilterEffect MakeOffsetFilterEffect(double dx, double dy, FilterInput? filterInput, string resultName = null)
        {
            return new OffsetFilterEffect(dx, dy, filterInput.ToString(), resultName);
        }

        public static OffsetFilterEffect MakeOffsetFilterEffect(double dx, double dy, string filterInput, string resultName = null)
        {
            return new OffsetFilterEffect(dx, dy, filterInput, resultName);
        }

        public static BlendFilterEffect MakeBlendFilterEffect(FilterInput filterInput, FilterInput filterInput2, BlendMode mode, string resultName = null)
        {
            return new BlendFilterEffect(filterInput.ToString(), filterInput2.ToString(), mode, resultName);
        }

        public static BlendFilterEffect MakeBlendFilterEffect(string filterInput, FilterInput filterInput2, BlendMode mode, string resultName = null)
        {
            return new BlendFilterEffect(filterInput, filterInput2.ToString(), mode, resultName);
        }

        public static BlendFilterEffect MakeBlendFilterEffect(FilterInput filterInput, string filterInput2, BlendMode mode, string resultName = null)
        {
            return new BlendFilterEffect(filterInput.ToString(), filterInput2, mode, resultName);
        }

        public static BlendFilterEffect MakeBlendFilterEffect(string filterInput, string filterInput2, BlendMode mode, string resultName = null)
        {
            return new BlendFilterEffect(filterInput, filterInput2, mode, resultName);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(double[] matrix)
        {
            return new ColorMatrixFilterEffect(matrix, null, null);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(string matrix)
        {
            return new ColorMatrixFilterEffect(matrix, null, null);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(double[] matrix, FilterInput filterInput, string resultName = null)
        {
            return new ColorMatrixFilterEffect(matrix, filterInput.ToString(), resultName);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(string matrix, FilterInput filterInput, string resultName = null)
        {
            return new ColorMatrixFilterEffect(matrix, filterInput.ToString(), resultName);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(double[] matrix, string filterInput, string resultName = null)
        {
            return new ColorMatrixFilterEffect(matrix, filterInput, resultName);
        }

        public static ColorMatrixFilterEffect MakeColorMatrixFilterEffect(string matrix, string filterInput, string resultName = null)
        {
            return new ColorMatrixFilterEffect(matrix, filterInput, resultName);
        }

        public static SpecularLightingFilterEffect MakeSpecularLightingFilterEffect(
            double surfaceScale,
            double specularConstant,
            double specularExponent,
            string lightingColor,
            string filterInputName = null,
            string resultName = null)
        {
            return new SpecularLightingFilterEffect(
                surfaceScale,
                specularConstant,
                specularExponent,
                lightingColor,
                filterInputName,
                resultName);
        }

        public static Filter MakeFilter(double? x = null, double? y = null, double? width = null, double? height = null)
        {
            return new Filter(x, y, width, height);
        }

        public static Group MakeGroup()
        {
            return new Group();
        }

        /// <summary>
        ///     Set the background fill for this SVG. The fill is created by creating a rectangle
        ///     covering the SVG extents. WithFill() is used to cascade the fill to child elements.
        /// </summary>
        /// <param name="backgroundFill"></param>
        /// <returns></returns>
        public Svg WithBackgroundFill(string backgroundFill)
        {
            _backgroundFill = backgroundFill;

            return this;
        }

        private class StringWriterWithEncoding : StringWriter
        {
            public StringWriterWithEncoding(Encoding encoding)
            {
                Encoding = encoding;
            }

            public override Encoding Encoding { get; }
        }
    }
}