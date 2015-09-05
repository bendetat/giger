using System.Collections.Generic;
using System.Linq;
using Giger.Charts.BarCharts;

namespace Giger.Charts.ColorGenerators
{
    /// <summary>
    /// Color generator based on the point index
    /// </summary>
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
}