using System;
using System.Linq;
using System.Xml;
using Giger.Plumbing;
using Giger.Shapes;
using Giger.Text;
using System.Collections.Generic;
using Giger.Charts.ColorGenerators;
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
        private double _stackLabelLineHeight = 16;
        private double _dataPointLabelHeight = 16;
        private double _groupLabelLineHeight = 16;
        private string _dataLabelFill = "black";
        private string _groupLabelFill = "black";
        private string _stackLabelFill = "black";
        private string _dataPointLabelFill = "black";
        private double _dataLabelWithinFingerThreshold = 20;
        private bool _showDataLabelOutsideFingerThreshold = false;
        private string _dataLabelFontFamily = FontFamilies.Helvetica;
        private string _dataPointLabelFontFamily = FontFamilies.Helvetica;
        private string _stackLabelFontFamily = FontFamilies.Helvetica;
        private string _groupLabelFontFamily = FontFamilies.Helvetica;
        private double _dataLabelFontSize = 10;
        private double _stackLabelFontSize = 12;
        private double _groupLabelFontSize = 14;
        private double _dataPointLabelFontSize = 10;
        private string _paperFill = "";
        private string _drawableFill = "none";
        private string _stroke = "";
        private double _strokeWidth = 0;
        private IDataPointColorGenerator _fingerColorGenerator = new RandomDataPointColorGenerator();
        private bool _alwaysShowDataLabelOutsideFingerThreshold = false;
        private IDataPointColorGenerator _fingerLabelColorGenerator;
        private bool _dataPointAutoLineSplit = false;
        private int _dataPointLengthPerLine = 14;
        private bool _groupLabelAutoLineSplit = false;
        private int _groupLabelLengthPerLine = 14;
        private bool _stackLabelAutoLineSplit = false;
        private int _stackLabelLengthPerLine = 14;

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
            if (!string.IsNullOrEmpty(_paperFill))
            {
                this
                    .Rectangle(X ?? 0 + _leftGutter, Y ?? 0 + _topGutter, Width ?? 0 - _leftGutter - _rightGutter, Height ?? 0 - _topGutter - _bottomGutter)
                    .WithFill(_paperFill);
            }

            // Hard-code some extra bottom padding if there are any labels  - just enough for descenders (p, q, etc)
            var anyGroupLabels = _data.Groups.Any(x => !string.IsNullOrEmpty(x.Label));
            var anyStackLabels = _data.Groups.Any(g => g.Stacks.Any(s => !string.IsNullOrEmpty(s.Label)));
            var anyDataPointLabels = _data.Groups.Any(g => g.Stacks.Any(s => s.DataPoints.Any(d => !string.IsNullOrEmpty(d.Label))));
            double extraBottomPadding = anyGroupLabels || anyStackLabels || anyDataPointLabels ? 4 : 0;

            // Extra top padding if the data labels are always shown outside the finger threshold
            var extraTopPadding = _alwaysShowDataLabelOutsideFingerThreshold ? 16 : 0;

            var drawableWidth = Width ?? 0 - _leftGutter - _rightGutter - _leftPadding - _rightPadding;
            var drawableHeight = Height ?? 0 - _topGutter - _bottomGutter - _topPadding - _bottomPadding - extraBottomPadding - extraTopPadding;
            var drawableLeft = X ?? 0 + _leftGutter + _leftPadding;
            var drawableTop = Y ?? 0 + _topGutter + _topPadding + extraTopPadding;

            this
                .Rectangle(drawableLeft, drawableTop, drawableWidth, drawableHeight)
                .WithFill(_drawableFill);

            var groupLabelHeight = GetMaxGroupLabelHeight();
            var stackLabelHeight = GetMaxStackLabelHeight();
            var dataPointLabelHeight = GetMaxDataPointLabelHeight();
            var chartHeight = drawableHeight - groupLabelHeight - stackLabelHeight - dataPointLabelHeight;
            var chartBottom = drawableTop + chartHeight;
            var groupCount = _data.Groups.Count();
            var stacksPerGroup = _data.Groups.Max(x => x.Stacks.Count());
            var widthPerGroup = (drawableWidth - _groupGutter*(groupCount - 1))/groupCount;
            var widthPerStack = (widthPerGroup - _stackGutter*(stacksPerGroup - 1))/stacksPerGroup;
            var maxValue = _data.Groups.Max(g => g.Stacks.Max(s => s.DataPoints.Sum(x => x.Value)));
            var heightPerValue = maxValue == 0 ? 0 : chartHeight/maxValue;

            var fingers =
                from @group in _data.Groups.Select((x, i) => new {Group = x, Index = i})
                let groupLabel = AddChild(GetGroupLabel(drawableLeft, widthPerGroup, @group.Index, chartBottom, stackLabelHeight, @group.Group.Label))
                from stack in @group.Group.Stacks.Select((x, i) => new {Stack = x, Index = i})
                let stackLeft = drawableLeft + widthPerGroup*@group.Index + _groupGutter*@group.Index + widthPerStack*stack.Index + _stackGutter*(stack.Index)
                let stackLabel = AddChild(GetStackLabel(stackLeft, widthPerStack, chartBottom, dataPointLabelHeight, stack.Stack.Label))
                from point in stack.Stack.DataPoints.Select((x, i) => new {Point = x, Index = i})
                let dataPointLabel = AddChild(GetDataPointLabel(stackLeft, widthPerStack, chartBottom + 4, point.Point.Label))
                let fingerBottom = chartBottom - stack.Stack.DataPoints.Take(point.Index).Sum(x => x.Value*heightPerValue)
                let fingerTop = fingerBottom - point.Point.Value*heightPerValue
                let finger = this.Rectangle(stackLeft, fingerTop, widthPerStack, fingerBottom - fingerTop)
                    .WithFill(_fingerColorGenerator.GenerateColor(@group.Index, stack.Index, point.Index, point.Point.Value))
                let fingerLabel = GetDataPointFingerLabel(stackLeft, widthPerStack, fingerTop, fingerBottom - fingerTop, point.Point.Value, @group.Index, stack.Index, point.Index)
                select 0;

            fingers.ToArray();

            if (!string.IsNullOrEmpty(_stroke))
            {
                this
                    .Rectangle(X ?? 0 + _leftGutter, Y ?? 0 + _topGutter, Width ?? 0 - _leftGutter - _rightGutter, Height ?? 0 - _topGutter - _bottomGutter)
                    .WithFill("none")
                    .WithStroke(_stroke)
                    .WithStrokeWidth(_strokeWidth);
            }

            return this;
        }

        BaseElement GetGroupLabel(double drawableLeft, double widthPerGroup, int index, double chartBottom, double stackLabelHeight, string label)
        {
            if (string.IsNullOrEmpty(label)) return new NoopElement();

            var element = new MultiLineText(drawableLeft + ((widthPerGroup + _groupGutter)*index) + widthPerGroup/2, chartBottom + stackLabelHeight + 4, label)
                .WithTextAnchor(TextAnchor.Middle)
                .WithFill(_groupLabelFill)
                .WithFontFamily(_groupLabelFontFamily)
                .WithFontSize(_groupLabelFontSize)
                .WithLineHeight(_groupLabelLineHeight);

            if (_groupLabelAutoLineSplit)
            {
                element.WithAutoLineSplit(_groupLabelLengthPerLine);
            }

            return element;
        }

        BaseElement GetStackLabel(double stackLeft, double widthPerStack, double chartBottom, double dataPointLabelHeight, string label)
        {
            if (string.IsNullOrEmpty(label)) return new NoopElement();

            var element = new MultiLineText(stackLeft + widthPerStack/2, chartBottom + dataPointLabelHeight + 4, label)
                .WithTextAnchor(TextAnchor.Middle)
                .WithFill(_stackLabelFill)
                .WithFontFamily(_stackLabelFontFamily)
                .WithFontSize(_stackLabelFontSize)
                .WithLineHeight(_stackLabelLineHeight);


            if (_stackLabelAutoLineSplit)
            {
                element.WithAutoLineSplit(_stackLabelLengthPerLine);
            }

            return element;
        }

        MultiLineText GetDataPointLabel(double stackLeft, double widthPerStack, double chartBottom, string label)
        {
            var element = new MultiLineText(stackLeft + widthPerStack/2, chartBottom, label)
                .WithTextAnchor(TextAnchor.Middle)
                .WithFill(_dataPointLabelFill)
                .WithFontFamily(_dataPointLabelFontFamily)
                .WithFontSize(_dataPointLabelFontSize)
                .WithLineHeight(_dataPointLabelHeight);

            if (_dataPointAutoLineSplit)
            {
                element.WithAutoLineSplit(_dataPointLengthPerLine);
            }


            return element;
        }

        double GetMaxDataPointLabelHeight()
        {
            return _data.Groups.Max(g => g.Stacks.Max(s => s.DataPoints.Max(p => GetDataPointLabel(1, 1, 1, p.Label).Height ?? 0)));
        }

        double GetMaxGroupLabelHeight()
        {
            return _data.Groups.Max(g => GetGroupLabel(1, 1, 1, 1, 1, g.Label).Height ?? 0);
        }

        double GetMaxStackLabelHeight()
        {
            return _data.Groups.Max(g => g.Stacks.Max(s => GetStackLabel(1, 1, 1, 1, s.Label).Height ?? 0));
        }

        private BaseElement GetDataPointFingerLabel(double stackLeft, double widthPerStack, double fingerTop, double fingerHeight, double value, int groupIndex, int stackIndex, int pointIndex)
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

        public VerticalBarChart WithStackLabelFill(string stackLabelFill)
        {
            _stackLabelFill = stackLabelFill;
            return this;
        }

        /// <summary>
        /// Set the fill for the data point label - shown at the bottom of the chart
        /// (ChartDataPoint.Label)
        /// </summary>
        /// <param name="dataPointLabelFill"></param>
        /// <returns></returns>
        public VerticalBarChart WithDataPointLabelFill(string dataPointLabelFill)
        {
            _dataPointLabelFill = dataPointLabelFill;
            return this;
        }

        /// <summary>
        /// Set the height of the data point label - shown at the bottom of the chart
        /// (ChartDataPoint.Label)
        /// </summary>
        /// <param name="dataPointLabelHeight"></param>
        /// <returns></returns>
        public VerticalBarChart WithDataPointLabelHeight(double dataPointLabelHeight)
        {
            _dataPointLabelHeight = dataPointLabelHeight;
            return this;
        }

        /// <summary>
        /// Set the font family for the data point label - shown at the bottom of the chart
        /// (ChartDataPoint.Label)
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        public VerticalBarChart WithDataPointLabelFontFamily(string fontFamily)
        {
            _dataPointLabelFontFamily = fontFamily;
            return this;
        }

        /// <summary>
        /// Set the font size for the data point label - shown at the bottom of the chart
        /// (ChartDataPoint.Label)
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public VerticalBarChart WithDataPointLabelFontSize(double fontSize)
        {
            _dataPointLabelFontSize = fontSize;
            return this;
        }

        /// <summary>
        /// Automatically split data point labels into lines, using the length per
        /// line as a guide (ChartDataPoint.Label)
        /// </summary>
        /// <param name="lengthPerLine"></param>
        /// <returns></returns>
        public VerticalBarChart WithDataPointAutoLineSplit(int lengthPerLine)
        {
            _dataPointAutoLineSplit = true;
            _dataPointLengthPerLine = lengthPerLine;
            return this;
        }

        /// <summary>
        /// The gutter between groups of stacks - default is 40
        /// </summary>
        /// <param name="groupGutter"></param>
        /// <returns></returns>
        public VerticalBarChart WithGroupGutter(double groupGutter)
        {
            _groupGutter = 20;
            return this;
        }

        /// <summary>
        /// Automatically split group labels into lines, using the length per line as a guide
        /// </summary>
        /// <param name="lengthPerLine">Maximum characters per line</param>
        /// <returns></returns>
        public VerticalBarChart WithGroupLabelAutoLineSplit(int lengthPerLine)
        {
            _groupLabelAutoLineSplit = true;
            _groupLabelLengthPerLine = lengthPerLine;

            return this;
        }

        /// <summary>
        /// Automatically split stack labels into lines, using the length per line as a guide
        /// </summary>
        /// <param name="lengthPerLine">Maximum characters per line</param>
        /// <returns></returns>
        public VerticalBarChart WithStackLabelAutoLineSplit(int lengthPerLine)
        {
            _stackLabelAutoLineSplit = true;
            _stackLabelLengthPerLine = lengthPerLine;

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
}