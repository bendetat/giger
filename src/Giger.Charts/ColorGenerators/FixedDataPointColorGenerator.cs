using System;

namespace Giger.Charts.ColorGenerators
{
    /// <summary>
    /// Fixed color generator, returning a single color for all data points
    /// </summary>
    public class FixedDataPointColorGenerator : IDataPointColorGenerator
    {
        readonly Func<string> _colorCallback;

        public FixedDataPointColorGenerator(string color)
        {
            _colorCallback = () => color;
        }

        public FixedDataPointColorGenerator(Func<string> colorCallback)
        {
            _colorCallback = colorCallback;
        }

        public string GenerateColor(int group, int stack, int point, double value)
        {
            return _colorCallback();
        }
    }
}