using System.Xml;
using Giger.Plumbing;
using Giger.Shapes;
using Giger.Text;

namespace Giger.Charts.Legends
{
    public class GridLegend : Element<GridLegend>
    {
        public const double ItemWidth = 110;
        public const double ItemHeight = 30;
        private string _fontFamily = FontFamilies.Helvetica;
        private double _fontSize = 12;

        public GridLegend(double x, double y) : base(x, y, null, null)
        {
        }

        public new double X => base.X ?? 0;
        public new double Y => base.Y ?? 0;

        public GridLegend AddLegendItem(int row, int column, string fill, string title)
        {
            var item = new GridLegendItem(
                this.X + column*ItemWidth, 
                this.Y + row*ItemHeight, 
                fill, 
                title,
                _fontFamily,
                _fontSize);

            this.AddChild(item);

            return this;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("g");
        }
    }

    public class GridLegendItem : Element<GridLegendItem>
    {
        public GridLegendItem(double x, double y, string fill, string title, string fontFamily,double fontSize)
            : base(x, y, null, null)
        {
            this
                .Rectangle(x, y, 10, 10)
                .WithFill(fill);
            this
                .Text(x + 18, y + 10, title)
                .WithFontFamily(fontFamily)
                .WithFontSize(fontSize);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("g");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static GridLegend GridLegend(this BaseElement baseElement, double x, double y)
        {
            var legend = new GridLegend(x, y);

            baseElement.AddChild(legend);

            return legend;
        }
    }
}