using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Chvart.Giger;
using Chvart.Plumbing;
using Chvart.Raphael;
using Simplicity;

namespace Chvart
{
    public static partial class ChvartExtensions
    {
        public static VerticalBarChart VerticalBarChart(this Raphael.Svg svg, double x, double y, double width, double height, IEnumerable<double> chartValues, VerticalBarChart.Options options = null)
        {
            var chart = new VerticalBarChart(x, y, width, height, chartValues, options);

            svg.AddChild(chart);

            return chart;
        }
        public static VerticalBarChart VerticalBarChart(this Raphael.Svg svg, double x, double y, double width, double height, IEnumerable<IEnumerable<double>> chartValues, VerticalBarChart.Options options = null)
        {
            var chart = new VerticalBarChart(x, y, width, height, chartValues, options);

            svg.AddChild(chart);

            return chart;
        }
    }

    public abstract class BarChart : ContainerElement
    {
        protected BarChart(double x, double y, double width, double height) : base(x, y, width, height)
        {
        }
    }

    public class VerticalBarChart : BarChart
    {
        private readonly IList<IList<Finger>> _bars = new List<IList<Finger>>();
        private readonly Options _options;
        private readonly double _total;
        private readonly List<double>[] _values;
        private int _columnsPerSet;
        private ContainerElement _labels;

        public VerticalBarChart(double x, double y, double width, double height, IEnumerable<double> chartValues, Options options = null)
            : this(x, y, width, height, FutzWith(chartValues), options)
        {
        }

        public VerticalBarChart(double xPrime, double yPrime, double width, double height, IEnumerable<IEnumerable<double>> chartValues, Options options = null)
            : base(xPrime, yPrime, width, height)
        {
            _options = options ?? new Options();
            var bars = 1;
            _values = TidyChartValues(chartValues);

            _total = _options.To ?? (_options.Stacked
                ? _values.Max(v => v.Sum())
                : _values.Max(v => v.Max()));

            var barWidth = width/(_values.Length*(100 + _options.Gutter) + _options.Gutter)*100;
            var barHorizontalGutter = barWidth*_options.Gutter/100;
            var x = xPrime + barHorizontalGutter;
            var y = (height - 2*_options.VerticalGutter)/_total;

            if (!_options.Stretch)
            {
                barHorizontalGutter = Math.Round(barHorizontalGutter);
                barWidth = Math.Floor(barWidth);
            }

            barWidth = barWidth/_columnsPerSet;

            foreach (var set in _values)
            {
                var stack = new List<Finger>();
                foreach (var value in set)
                {
                    var h = Math.Round(value*y);
                    var top = y + height - _options.VerticalGutter - h;
                    var bar = new Finger(
                        Math.Round(x + barWidth/2),
                        top + h,
                        barWidth,
                        h,
                        Direction.Vertical,
                        _options.Type,
                        value);
                    bar
                        .SetAttr(new
                        {
                            stroke = "none",
                            fill = "todo"
                        });

                    stack.Add(bar);
                    this.AddChild(bar);
                }
                _bars.Add(stack);

                // I have no idea what is going on here
                if (_options.Stacked)
                {
                    //    var cvr;

                    //    covers2.push(cvr = paper.rect(stack[0].x - stack[0].w / 2, y, barwidth, height).attr(chartinst.shim));
                    //    cvr.bars = paper.set();

                    //    var size = 0;

                    //    for (var s = stack.length; s--;)
                    //    {
                    //        stack[s].toFront();
                    //    }

                    //    for (var s = 0, ss = stack.length; s < ss; s++)
                    //    {
                    //        var bar = stack[s],
                    //            cover,
                    //            h = (size + bar.value) * Y,
                    //            path = finger(bar.x, y + height - barvgutter - !!size * .5, barwidth, h, true, type, 1, paper);

                    //        cvr.bars.push(bar);
                    //        size && bar.attr({ path: path});
                    //    bar.h = h;
                    //    bar.y = y + height - barvgutter - !!size * .5 - h;
                    //    covers.push(cover = paper.rect(bar.x - bar.w / 2, bar.y, barwidth, bar.value * Y).attr(chartinst.shim));
                    //    cover.bar = bar;
                    //    cover.value = bar.value;
                    //    size += bar.value;
                    //}

                    //X += barwidth;
                }

                x += barHorizontalGutter;
            }
            //covers2.toFront;
            xPrime = x + barHorizontalGutter;

            //    if (!opts.stacked)
            //    {
            //        for (var i = 0; i < len; i++)
            //        {
            //            for (var j = 0; j < (multi || 1); j++)
            //            {
            //                var cover;

            //                covers.push(cover = paper.rect(Math.round(X), y + barvgutter, barwidth, height - barvgutter).attr(chartinst.shim));
            //                cover.bar = multi ? bars[j][i] : bars[i];
            //                cover.value = cover.bar.value;
            //                X += barwidth;
            //            }

            //            X += barhgutter;
            //        }
            //    }
        }

        private int ColumnsPerSet => _values.Max(x => x.Count());

        private static IEnumerable<IEnumerable<double>> FutzWith(IEnumerable<double> values)
        {
            return values.Select(x => new[] {x});
        }

        private List<double>[] TidyChartValues(IEnumerable<IEnumerable<double>> chartValues)
        {
            var values = chartValues.Select(v => new List<double>(v)).ToArray();

            var columnsPerSet = values.Max(v => v.Count());
            foreach (var set in values)
            {
                while (set.Count() < _columnsPerSet)
                {
                    set.Add(0);
                }
            }
            return values;
        }

        public void SetLabels(string[] labels, bool isBottom)
        {
            this._labels = new ContainerElement();
            this.AddChild(_labels);

            Text L;
            double l = double.MinValue;

            if (_options.Stacked)
            {
                for (var i = 0; i < _values.Count(); i++)
                {
                    var set = _values[i];
                    double tot = set.Sum();
                    var label = labels[i].Labelise(tot, _total);
                    var bar = _bars[i].Last();
                    L = this.Text(bar.X, this.Y + this.Height - _options.VerticalGutter/2, label)
                        .SetAttr(_options.TxtAttr)
                        ; //.InsertBefore(covers[i + set.Count]);
                    if (L.X - 7 < 1)
                    {
                        L.Remove();
                    }
                    else
                    {
                        _labels.AddChild(L);
                        l = L.X + L.Width;
                    }
                }
            }
            else
            {
                for (var i = 0; i < _values.Count(); i++)
                {
                    var set = _values[i];
                    for (var j = 0; j < set.Count; j++)
                    {
                        // todo multi-dimensional labels (labels[i][j]) and values
                        var label = labels[j].Labelise(_values[i].Sum(), _total);
                        var bar = _bars[i][j];
                        L = this.Text(bar.X, isBottom ? this.Y + this.Height - _options.VerticalGutter/2 : bar.Y - 10, label)
                            .SetAttr(_options.TxtAttr);
                        //.InsertBefore(covers[i * (multi || 1) + j]);
                        if (L.X - 7 < 1)
                        {
                            L.Remove();
                        }
                        else
                        {
                            _labels.AddChild(L);
                            l = L.X + L.Width;
                        }
                    }
                }
            }
        }

        //public class HorizontalBarChart
        //{
        //}


        public class Options
        {
            public Options()
            {
                Type = Ending.Square;
                Gutter = 0.2;
                VerticalGutter = 20;
                Stacked = false;
                Stretch = false;
                TxtAttr = null;
            }

            public Ending Type { get; set; }
            public double Gutter { get; set; }
            public double VerticalGutter { get; set; }
            public bool Stacked { get; set; }
            public double? To { get; set; }
            public bool Stretch { get; set; }
            public object TxtAttr { get; set; }
        }
    }

    public class PathElement : Element<PathElement>
    {
        public PathElement(Path path)
            : base(0, 0, 0, 0)
        {
            SetAttr(new
            {
                d = path.ToString()
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateElement("path");
        }
    }

    public class Finger : PathElement
    {
        private double _value;

        public Finger(double x, double y, double width, double height, Direction dir, Ending ending, double value)
            : base(GetPath(x, y, width, height, dir, ending))
        {
            _value = value;
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        private static Path GetPath(double x, double y, double width, double height, Direction dir, Ending ending)
        {
            x = Math.Round(x);
            y = Math.Round(y);
            width = Math.Round(width);
            height = Math.Round(height);

            if ((dir == Direction.Horizontal && height == 0) || (dir == Direction.Vertical && width == 0))
            {
                return Path.Empty;
            }

            return ending.Match()
                .With(Ending.Round, _ => GetRoundPath(x, y, width, height, dir))
                .With(Ending.Sharp, _ => GetSharpPath(x, y, width, height, dir))
                .With(Ending.Square, _ => GetSquarePath(x, y, width, height, dir))
                .With(Ending.Soft, _ => GetSoftPath(x, y, width, height, dir))
                .Else(Path.Empty);
        }

        private static Path GetSoftPath(double x, double y, double width, double height, Direction dir)
        {
            if (dir == Direction.Horizontal)
            {
                var r = Math.Min(width, Math.Round(height/5));

                return new Path(
                    "M", x + .5, y + .5 - Math.Floor(height/2),
                    "l", width - r, 0,
                    "a", r, r, 0, 0, 1, r, r,
                    "l", 0, height - r*2,
                    "a", r, r, 0, 0, 1, -r, r,
                    "l", r - width, 0,
                    "z");
            }
            else
            {
                var r = Math.Min(Math.Round(width/5), height);

                return new Path(
                    "M", x - Math.Floor(width/2), y,
                    "l", 0, r - height,
                    "a", r, r, 0, 0, 1, r, -r,
                    "l", width - 2*r, 0,
                    "a", r, r, 0, 0, 1, r, r,
                    "l", 0, height - r,
                    "z");
            }
        }

        private static Path GetSquarePath(double x, double y, double width, double height, Direction dir)
        {
            if (dir == Direction.Horizontal)
            {
                return new Path(
                    "M", x, y + Math.Floor(height/2),
                    "l", 0, -height, width, 0, 0, height,
                    "z");
            }
            else
            {
                return new Path(
                    "M", x + Math.Floor(width/2), y,
                    "l", 1 - width, 0, 0, -height, width - 1, 0,
                    "z");
            }
        }

        private static Path GetSharpPath(double x, double y, double width, double height, Direction dir)
        {
            if (dir == Direction.Horizontal)
            {
                var half = Math.Floor(height/2);

                return new Path(
                    "M", x, y + half,
                    "l", 0, -height, Math.Max(width - half, 0), 0, Math.Min(half, width), half, -Math.Min(half, width), half + (half*2 - height), /* In the original it is `half + (half * 2 < height)` which is invalid */
                    "z");
            }
            else
            {
                var half = Math.Floor(width/2);

                return new Path(
                    "M", x + half, y,
                    "l", -width, 0, 0, -Math.Max(height - half, 0), half, -Math.Max(half, height), half, Math.Min(half, height), half,
                    "z");
            }
        }

        private static Path GetRoundPath(double x, double y, double width, double height, Direction dir)
        {
            var r = Math.Floor(height/2);

            if (dir == Direction.Horizontal)
            {
                if (width < r)
                {
                    r = width;

                    return new Path(
                        "M", x + .5, y + .5 - Math.Floor(height/2),
                        "l", 0, 0,
                        "a", r, Math.Floor(height/2), 0, 0, 1, 0, height,
                        "l", 0, 0,
                        "z");
                }
                else
                {
                    return new Path(
                        "M", x + .5, y + .5 - r,
                        "l", width - r, 0,
                        "a", r, r, 0, 1, 1, 0, height,
                        "l", r - width, 0,
                        "z");
                }
            }
            else
            {
                r = Math.Floor(width/2);

                if (height < r)
                {
                    r = height;

                    return new Path(
                        "M", x - Math.Floor(width/2), y,
                        "l", 0, 0,
                        "a", Math.Floor(width/2), r, 0, 0, 1, width, 0,
                        "l", 0, 0,
                        "z");
                }
                else
                {
                    return new Path(
                        "M", x - r, y,
                        "l", 0, r - height,
                        "a", r, r, 0, 1, 1, width, 0,
                        "l", 0, height - r,
                        "z");
                }
            }
        }
    }

    public class Path
    {
        private readonly object[] _path;

        public Path(params object[] path)
        {
            _path = path;
        }

        public static Path Empty => new Path();

        public override string ToString()
        {
            if (!_path.Any())
            {
                return string.Empty;
            }
            if (!(_path.First() is string))
            {
                throw new InvalidOperationException("Path does not start with a command");
            }

            var commands = new List<Command>();
            var currentCommand = new Command("ERROR");

            foreach (var item in _path)
            {
                if (item is string)
                {
                    currentCommand = new Command(item.ToString());
                    commands.Add(currentCommand);
                }
                else
                {
                    currentCommand.Parameters.Add(item);
                }
            }

            return string.Join("", commands);
        }

        private class Command
        {
            private readonly string _commandName;

            public Command(string commandName)
            {
                _commandName = commandName;

                Parameters = new List<object>();
            }

            public IList<object> Parameters { get; }

            public override string ToString()
            {
                var parameters = string.Join(",", Parameters);

                return $"{_commandName}{parameters}";
            }
        }
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public enum Ending
    {
        Round,
        Sharp,
        Soft,
        Square
    }
}