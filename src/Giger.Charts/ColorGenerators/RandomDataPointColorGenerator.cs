using System;
using Giger.Charts.BarCharts;

namespace Giger.Charts.ColorGenerators
{
    /// <summary>
    /// Random color generator, using a hash of the group, stack, point and value as the
    /// seed so the colors will be constant for given inputs.
    /// </summary>
    public class RandomDataPointColorGenerator : IDataPointColorGenerator
    {
        public string GenerateColor(int group, int stack, int point, double value)
        {
            var rand = new Random(group.GetHashCode() | stack.GetHashCode() | point.GetHashCode() | value.GetHashCode());

            return $"rgb({rand.Next(0, 255)},{rand.Next(0, 255)},{rand.Next(0, 255)})";
        }
    }
}