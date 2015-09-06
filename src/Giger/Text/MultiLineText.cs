using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Text
{
    /// <summary>
    /// A multi-line text element. Splits the input text by line and renders the text
    /// down from the origin point.
    /// </summary>
    public class MultiLineText : Element<MultiLineText>, IManualDraw<MultiLineText>
    {
        private readonly string _text;
        private double _lineHeight = 24;
        private bool _autoLineSplit = false;
        private int _lengthPerLine = Int32.MaxValue;

        public MultiLineText(double x, double y, string text) : base(x,y,null,null)
        {
            _text = text;
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("g");
        }

        IEnumerable<string> GetLines(string text)
        {
            if (text == null)
            {
                yield break;
            }

            using (var reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        IEnumerable<string> Lines => _autoLineSplit
                ? SplitIntoLines(_text, _lengthPerLine)
                : GetLines(_text);

        public MultiLineText Draw()
        {
            var textLines =
                from line in Lines.Select((x, i) => new {Line = x, Index = i})
                let lineY = (this.Y ?? 0) + (line.Index)*_lineHeight + _lineHeight/2
                select new Text((this.X ?? 0), lineY, line.Line);

            AddChildren(textLines);

            return this;
        }

        /// <summary>
        /// The height of the multi-line text element, calculated using the number of
        /// lines and the line height
        /// </summary>
        public override double? Height => Lines.Count()*_lineHeight;

        /// <summary>
        /// Set the height per line (default is 24)
        /// </summary>
        /// <param name="lineHeight"></param>
        /// <returns></returns>
        public MultiLineText WithLineHeight(double lineHeight)
        {
            _lineHeight = lineHeight;
            return this;
        }

        /// <summary>
        /// Automatically split the input text into lines, using the length per line
        /// as a guide.
        /// </summary>
        /// <param name="lengthPerLine">The maximum number of characters per line</param>
        /// <returns></returns>
        public MultiLineText WithAutoLineSplit(int lengthPerLine)
        {
            _autoLineSplit = true;
            _lengthPerLine = lengthPerLine;

            return this;
        }
        
        static IEnumerable<string> SplitIntoLines(string text, int lengthPerLine)
        {
            var words = text.Split(' ');

            var line = "";
            foreach (var word in words)
            {
                var currentWord = word;

                if (!string.IsNullOrEmpty(line) && line.Length + currentWord.Length + 1 > lengthPerLine)
                {
                    yield return line;
                    line = "";
                }

                while (currentWord.Length > lengthPerLine)
                {
                    var subword = currentWord.Substring(0, lengthPerLine - 1);
                    yield return subword + "-";
                    currentWord = currentWord.Substring(lengthPerLine - 1);
                }

                if (!string.IsNullOrEmpty(line))
                {
                    line += " ";
                }
                line += currentWord;
            }
            if (!string.IsNullOrEmpty(line))
            {
                yield return line;
            }
        }
    }

    public static partial class BaseElementExtensions
    {
        /// <summary>
        /// Create a multi-line text element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MultiLineText MultiLineText(this BaseElement element, double x, double y, string text)
        {
            var textElement = new MultiLineText(x, y, text);

            element.AddChild(textElement);

            return textElement;
        }
    }
}