using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Shapes
{
    public class Path : Element<Path>
    {
        private readonly Queue<PathCommand> _commands = new Queue<PathCommand>();
        private readonly string _commandString;

        public Path()
        {
        }

        public Path(string commandString)
        {
            _commandString = commandString;
        }

        private IEnumerable<Point> GetAllPoints => _commands.OfType<IHavePoints>().SelectMany(x => x.Points);

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            SetAttr(new
            {
                d = _commands.Any() ?
                    string.Join(string.Empty, _commands.Select(x => x.ToString()))
                    : _commandString
            });

            return doc.CreateSvgElement("path");
        }

        private Path AddCommand(PathCommand command)
        {
            _commands.Enqueue(command);

            SetX(GetAllPoints.Min(x => x.X));
            SetY(GetAllPoints.Min(x => x.Y));
            SetWidth(GetAllPoints.Max(x => x.X) - (Width??0));
            SetHeight(GetAllPoints.Max(x => x.Y) - (Height??0));

            return this;
        }

        public Path QuadraticBezier(double controlPointX, double controlPointY, double x, double y)
        {
            return AddCommand(new QuadraticBezierPathCommand(controlPointX, controlPointY, x, y));
        }

        public Path QuadraticBezierRelative(double controlPointX, double controlPointY, double x, double y)
        {
            return AddCommand(new QuadraticBezierRelativePathCommand(controlPointX, controlPointY, x, y));
        }

        public Path MoveTo(double x, double y)
        {
            return AddCommand(new MoveToPathCommand(x, y));
        }

        public Path MoveToRelative(double x, double y)
        {
            return AddCommand(new MoveToRelativePathCommand(x, y));
        }

        public Path LineTo(double x, double y)
        {
            return AddCommand(new LineToPathCommand(x, y));
        }

        public Path LineToRelative(double x, double y)
        {
            return AddCommand(new LineToRelativePathCommand(x, y));
        }

        public Path ClosePath()
        {
            AddCommand(new ClosePathCommand());

            return this;
        }

        private class QuadraticBezierPathCommand : PathCommand, IHavePoints
        {
            protected readonly double ControlPointX;
            protected readonly double ControlPointY;
            protected readonly double X;
            protected readonly double Y;

            public QuadraticBezierPathCommand(double controlPointX, double controlPointY, double x, double y)
            {
                ControlPointX = controlPointX;
                ControlPointY = controlPointY;
                X = x;
                Y = y;
            }

            public IEnumerable<Point> Points
            {
                get
                {
                    yield return new Point(ControlPointX, ControlPointY);
                    yield return new Point(X, Y);
                }
            }

            public override string ToString()
            {
                return $"Q{ControlPointX} {ControlPointY} {X} {Y}";
            }
        }

        private class QuadraticBezierRelativePathCommand : QuadraticBezierPathCommand
        {
            public QuadraticBezierRelativePathCommand(double controlPointX, double controlPointY, double x, double y) : base(controlPointX, controlPointY, x, y)
            {
            }

            public override string ToString()
            {
                return $"q{ControlPointX} {ControlPointY} {X} {Y}";
            }
        }

        private interface IHavePoints
        {
            IEnumerable<Point> Points { get; }
        }

        private abstract class PathCommand
        {
        }

        private class ClosePathCommand : PathCommand
        {
            public override string ToString()
            {
                return "Z";
            }
        }

        private abstract class PointPathCommand : PathCommand, IHavePoints
        {
            protected PointPathCommand(double x, double y)
            {
                X = x;
                Y = y;
            }

            protected double X { get; }
            protected double Y { get; }

            public IEnumerable<Point> Points
            {
                get { yield return new Point(X, Y); }
            }
        }

        private class MoveToPathCommand : PointPathCommand
        {
            public MoveToPathCommand(double x, double y) : base(x, y)
            {
            }

            public override string ToString()
            {
                return $"M{X} {Y} ";
            }
        }

        private class MoveToRelativePathCommand : PointPathCommand
        {
            public MoveToRelativePathCommand(double x, double y) : base(x, y)
            {
            }

            public override string ToString()
            {
                return $"m{X} {Y} ";
            }
        }

        private class LineToPathCommand : PointPathCommand
        {
            public LineToPathCommand(double x, double y) : base(x, y)
            {
            }

            public override string ToString()
            {
                return $"L{X} {Y} ";
            }
        }

        private class LineToRelativePathCommand : PointPathCommand
        {
            public LineToRelativePathCommand(double x, double y) : base(x, y)
            {
            }

            public override string ToString()
            {
                return $"l{X} {Y} ";
            }
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Path Path(this BaseElement baseElement)
        {
            var path = new Path();

            baseElement.AddChild(path);

            return path;
        }

        public static Path Path(this BaseElement baseElement, string commandString)
        {
            var path = new Path(commandString);

            baseElement.AddChild(path);

            return path;
        }
    }
}