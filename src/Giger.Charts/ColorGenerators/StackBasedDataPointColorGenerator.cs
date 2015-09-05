using System.Collections.Generic;
using System.Linq;
using Giger.Charts.BarCharts;

namespace Giger.Charts.ColorGenerators
{
    /// <summary>
    /// Color generator based on the stack index
    /// </summary>
    public class StackBasedDataPointColorGenerator : IDataPointColorGenerator
    {
        readonly string[] _colors;

        public StackBasedDataPointColorGenerator(IEnumerable<string> colors)
        {
            _colors = colors.ToArray();
        }

        public string GenerateColor(int group, int stack, int point, double value)
        {
            return _colors[stack % _colors.Count()];
        }
    }
}