using System.Collections.Generic;
using System.Linq;

namespace Giger.Charts.ColorGenerators
{
    /// <summary>
    ///     Color generator based on the group index
    /// </summary>
    public class GroupBasedDataPointColorGenerator : IDataPointColorGenerator
    {
        private readonly string[] _colors;

        public GroupBasedDataPointColorGenerator(IEnumerable<string> colors)
        {
            _colors = colors.ToArray();
        }

        public string GenerateColor(int group, int stack, int point, double value)
        {
            return _colors[group%_colors.Count()];
        }
    }
}