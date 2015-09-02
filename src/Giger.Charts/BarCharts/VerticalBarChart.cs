using System;
using System.Linq;
using System.Xml;
using Giger.Plumbing;
using Giger.Shapes;
using Giger.Text;
using System.Collections.Generic;
using Giger.Charts.Legends;

namespace Giger.Charts.BarCharts
{
    public class VerticalBarChart : Element<VerticalBarChart>, IManualDraw<VerticalBarChart>
    {
        private const double DefaultGutter = 0;
        private const double DefaultPadding = 0;

        private double _leftGutter = DefaultGutter;
        private double _rightGutter = DefaultGutter;
        private double _topGutter = DefaultGutter;
        private double _bottomGutter = DefaultGutter;
        private double _leftPadding = DefaultPadding;
        private double _rightPadding = DefaultPadding;
        private double _topPadding = DefaultPadding;
        private double _bottomPadding = DefaultPadding;
        private readonly BarChartData _data;
        private double _groupGutter = 40;
        private double _stackGutter = 5;
        private string _dataLabelFormat = String.Empty;
        private double _fingerLabelHeight = 16;
        private double _stackLabelHeight = 16;
        private double _groupLabelHeight = 20;
        private string _dataLabelFill = "black";
        private string _groupLabelFill = "black";
        private string _stackLabelFill = "black";
        private double _dataLabelWithinFingerThreshold = 20;
        private bool _showDataLabelOutsideFingerThreshold = false;
        private string _dataLabelFontFamily = FontFamilies.Helvetica;
        private string _stackLabelFontFamily = FontFamilies.Helvetica;
        private string _groupLabelFontFamily = FontFamilies.Helvetica;
        private double _dataLabelFontSize = 10;
        private double _stackLabelFontSize = 12;
        private double _groupLabelFontSize = 14;
        private string _paperFill = "none";
        private string _drawableFill = "none";
        private string _stroke = "";
        private double _strokeWidth = 0;
        private IDataPointColorGenerator _fingerColorGenerator = new RandomDataPointColorGenerator();
        private bool _alwaysShowDataLabelOutsideFingerThreshold = false;
        private IDataPointColorGenerator _fingerLabelColorGenerator;

        public VerticalBarChart(double width, double height, BarChartData data)
            : this(0, 0, width, height, data)
        {
        }

        public VerticalBarChart(double x, double y, double width, double height, BarChartData data)
            : base(x, y, width, height)
        {
            _data = data;
            _fingerLabelColorGenerator = new FixedDataPointColorGenerator(() => _dataLabelFill);
        }

        public VerticalBarChart Draw()
        {
            this
                .Rectangle(X + _leftGutter, Y + _topGutter, Width - _leftGutter - _rightGutter, Height - _topGutter - _bottomGutter)
                .WithFill(_paperFill)
                .WithStroke(_stroke)
                .WithStrokeWidth(_strokeWidth);

            // Hard-code some extra bottom padding if there are any labels  - just enough for descenders (p, q, etc)
            var anyGroupLabels = _data.Groups.Any(x => !string.IsNullOrEmpty(x.Label));
            var anyStackLabels = _data.Groups.Any(g => g.Stacks.Any(s => !string.IsNullOrEmpty(s.Label)));
            double extraBottomPadding = anyGroupLabels || anyStackLabels ? 4 : 0;

            // Extra top padding if the data labels are always shown outside the finger threshold
            var extraTopPadding = _alwaysShowDataLabelOutsideFingerThreshold ? 16 : 0;

            var drawableWidth = Width - _leftGutter - _rightGutter - _leftPadding - _rightPadding;
            var drawableHeight = Height - _topGutter - _bottomGutter - _topPadding - _bottomPadding - extraBottomPadding - extraTopPadding;
            var drawableLeft = X + _leftGutter + _leftPadding;
            var drawableTop = Y + _topGutter + _topPadding + extraTopPadding;

            this
                .Rectangle(drawableLeft, drawableTop, drawableWidth, drawableHeight)
                .WithFill(_drawableFill);

            var groupLabelHeight = anyGroupLabels ? _groupLabelHeight : 0;
            var stackLabelHeight = anyStackLabels ? _stackLabelHeight : 0;
            var chartHeight = drawableHeight - groupLabelHeight - stackLabelHeight;
            var chartBottom = drawableTop + chartHeight;
            var groupCount = _data.Groups.Count();
            var stacksPerGroup = _data.Groups.Max(x => x.Stacks.Count());
            var widthPerGroup = (drawableWidth - _groupGutter*(groupCount - 1))/groupCount;
            var widthPerStack = (widthPerGroup - _stackGutter*(stacksPerGroup - 1))/stacksPerGroup;
            var maxValue = _data.Groups.Max(g => g.Stacks.Max(s => s.DataPoints.Sum(x => x.Value)));
            var heightPerValue = maxValue == 0 ? 0 : chartHeight/maxValue;

            var fingers =
                from @group in _data.Groups.Select((x, i) => new {Group = x, Index = i})
                let groupLabel = this.Text(drawableLeft + ((widthPerGroup + _groupGutter)*@group.Index) + widthPerGroup/2, chartBottom + stackLabelHeight + groupLabelHeight, @group.Group.Label)
                    .WithTextAnchor(TextAnchor.Middle)
                    .WithFill(_groupLabelFill)
                    .WithFontFamily(_groupLabelFontFamily)
                    .WithFontSize(_groupLabelFontSize)
                from stack in @group.Group.Stacks.Select((x, i) => new {Stack = x, Index = i})
                let stackLeft = drawableLeft + widthPerGroup*@group.Index + _groupGutter*@group.Index + widthPerStack*stack.Index + _stackGutter*(stack.Index)
                let stackLabel = this.Text(stackLeft + widthPerStack/2, chartBottom + stackLabelHeight, stack.Stack.Label)
                    .WithTextAnchor(TextAnchor.Middle)
                    .WithFill(_stackLabelFill)
                    .WithFontFamily(_stackLabelFontFamily)
                    .WithFontSize(_stackLabelFontSize)
                from point in stack.Stack.DataPoints.Select((x, i) => new {Point = x, Index = i})
                let fingerBottom = chartBottom - stack.Stack.DataPoints.Take(point.Index).Sum(x => x.Value*heightPerValue)
                let fingerTop = fingerBottom - point.Point.Value*heightPerValue
                let finger = this.Rectangle(stackLeft, fingerTop, widthPerStack, fingerBottom - fingerTop)
                    .WithFill(_fingerColorGenerator.GenerateColor(@group.Index, stack.Index, point.Index, point.Point.Value))
                let fingerLabel = GetFingerLabel(stackLeft, widthPerStack, fingerTop, fingerBottom - fingerTop, point.Point.Value, @group.Index, stack.Index, point.Index)
                select 0;

            fingers.ToArray();

            return this;
        }

        private BaseElement GetFingerLabel(double stackLeft, double widthPerStack, double fingerTop, double fingerHeight, double value, int groupIndex, int stackIndex, int pointIndex)
        {
            if (string.IsNullOrEmpty(_dataLabelFormat))
            {
                return this.Noop();
            }

            if (fingerHeight < _dataLabelWithinFingerThreshold && !_showDataLabelOutsideFingerThreshold && !_alwaysShowDataLabelOutsideFingerThreshold)
            {
                return this.Noop();
            }

            var y = _alwaysShowDataLabelOutsideFingerThreshold || fingerHeight < _dataLabelWithinFingerThreshold ? fingerTop - _fingerLabelHeight/2 : fingerTop + _fingerLabelHeight;

            return this
                .Text(stackLeft + widthPerStack/2, y, string.Format(_dataLabelFormat, value))
                .WithTextAnchor(TextAnchor.Middle)
                .WithFill(_fingerLabelColorGenerator.GenerateColor(groupIndex, stackIndex, pointIndex, value))
                .WithFontFamily(_dataLabelFontFamily)
                .WithFontSize(_dataLabelFontSize);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("g");
        }

        public new double X => base.X ?? 0;
        public new double Width => base.Width ?? 0;
        public new double Y => base.Y ?? 0;
        public new double Height => base.Height ?? 0;

        public VerticalBarChart WithGutter(double gutter)
        {
            _leftGutter = gutter;
            _rightGutter = gutter;
            _topGutter = gutter;
            _bottomGutter = gutter;
            return this;
        }

        public VerticalBarChart WithHorizontalGutter(double gutter)
        {
            _leftGutter = gutter;
            _rightGutter = gutter;
            return this;
        }

        public VerticalBarChart WithVerticalGutter(double gutter)
        {
            _topGutter = gutter;
            _bottomGutter = gutter;
            return this;
        }

        public VerticalBarChart WithDataLabelFormat(string dataLabelFormat)
        {
            this._dataLabelFormat = dataLabelFormat;
            return this;
        }

        public VerticalBarChart ShowDataLabelOutsideItem()
        {
            _showDataLabelOutsideFingerThreshold = true;
            return this;
        }

        public override VerticalBarChart WithFill(string fill)
        {
            _paperFill = fill;
            return this;
        }

        public VerticalBarChart WithPadding(double padding)
        {
            _leftPadding = _rightPadding = _topPadding = _bottomPadding = padding;
            return this;
        }

        public VerticalBarChart WithDrawableFill(string fill)
        {
            _drawableFill = fill;
            return this;
        }

        public override VerticalBarChart WithStroke(string stroke)
        {
            _stroke = stroke;
            return this;
        }

        public override VerticalBarChart WithStrokeWidth(double strokeWidth)
        {
            _strokeWidth = strokeWidth;
            return this;
        }

        public VerticalBarChart WithPointColorGenerator(IDataPointColorGenerator colorGenerator)
        {
            _fingerColorGenerator = colorGenerator;
            return this;
        }

        public VerticalBarChart WithPointLabelColorGenerator(IDataPointColorGenerator colorGenerator)
        {
            _fingerLabelColorGenerator = colorGenerator;
            return this;
        }

        public VerticalBarChart AlwaysShowDataLabel()
        {
            _alwaysShowDataLabelOutsideFingerThreshold = true;
            return this;
        }

        public VerticalBarChart WithTopGutter(double gutter)
        {
            _topGutter = gutter;
            return this;
        }

        public VerticalBarChart WithBottomGutter(double gutter)
        {
            _bottomGutter = gutter;
            return this;
        }

        public VerticalBarChart WithLeftGutter(double gutter)
        {
            _leftGutter = gutter;
            return this;
        }

        public VerticalBarChart WithRightGutter(double gutter)
        {
            _rightGutter = gutter;
            return this;
        }
    }

    public static partial class BaseElementExtensions
    {
        public static VerticalBarChart VerticalBarChart(this BaseElement baseElement, BarChartData data)
        {
            if (baseElement.Width == null || baseElement.Height == null)
            {
                throw new ArgumentOutOfRangeException(nameof(baseElement), "The base element requires a height and width for the chart to fill");
            }

            return VerticalBarChart(baseElement, baseElement.Width.Value, baseElement.Height.Value, data);
        }

        public static VerticalBarChart VerticalBarChart(this BaseElement baseElement, double width, double height, BarChartData data)
        {
            return VerticalBarChart(baseElement, 0, 0, width, height, data);
        }

        public static VerticalBarChart VerticalBarChart(this BaseElement baseElement, double x, double y, double width, double height, BarChartData data)
        {
            var chart = new VerticalBarChart(x, y, width, height, data);

            baseElement.AddChild(chart);

            return chart;
        }
    }

    public interface IDataPointColorGenerator {
    	string GenerateColor(int group, int stack, int point, double value);
    }

    public class FixedDataPointColorGenerator :IDataPointColorGenerator {
    	readonly Func<string> _colorCallback;

    	public FixedDataPointColorGenerator(string color) {
    		_colorCallback = () => color;
    	}

    	public FixedDataPointColorGenerator(Func<string> colorCallback) {
    		_colorCallback = colorCallback;
    	}

        public string GenerateColor(int group, int stack, int point, double value)
		{
			return _colorCallback();
		}
    }

    public class RandomDataPointColorGenerator : IDataPointColorGenerator {
        public string GenerateColor(int group, int stack, int point, double value)
        {
            var rand = new Random(group.GetHashCode() | stack.GetHashCode() | point.GetHashCode() | value.GetHashCode());

            return $"rgb({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)})";
        } 
    }

    public class StackBasedDataPointColorGenerator  : IDataPointColorGenerator {
        readonly string[] _colors;

    	public StackBasedDataPointColorGenerator(IEnumerable<string> colors) {
    		_colors = colors.ToArray();
    	}

    	public string GenerateColor(int group, int stack, int point, double value)
    	{
    		return _colors[stack % _colors.Count()];
    	}
    }

    public class PointBasedDataPointColorGenerator : IDataPointColorGenerator
    {
        readonly string[] _colors;

        public PointBasedDataPointColorGenerator(IEnumerable<string> colors)
        {
            _colors = colors.ToArray();
        }

        public string GenerateColor(int group, int stack, int point, double value)
        {
            return _colors[point % _colors.Count()];
        }
    }

    public class GroupBasedDataPointColorGenerator : IDataPointColorGenerator
    {
        readonly string[] _colors;

        public GroupBasedDataPointColorGenerator(IEnumerable<string> colors)
        {
            _colors = colors.ToArray();
        }

        public string GenerateColor(int group, int stack, int point, double value)
        {
            return _colors[group % _colors.Count()];
        }
    }


}