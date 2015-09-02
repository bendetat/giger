namespace Giger.Charts
{
    public class ChartDataPoint
    {
        public ChartDataPoint(double value) : this(value, null)
        {
        }

        public ChartDataPoint(double value, string label)
        {
            Value = value;
            Label = label;
        }

        public double Value { get; private set; }
        public string Label { get; private set; }
    }
}