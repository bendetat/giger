using System.Collections.Generic;
using System.Linq;

namespace Giger.Charts.BarCharts
{
    public class BarChartStackData
    {
        public BarChartStackData(double value) : this(value, null)
        {
        }

        public BarChartStackData(double value, string label) : this(new ChartDataPoint(value, label))
        {
        }

        public BarChartStackData(ChartDataPoint dataPoint)
            : this(new[] {dataPoint})
        {
        }

        public BarChartStackData(IEnumerable<ChartDataPoint> dataPoints)
            : this(dataPoints, null)
        {
        }

        public BarChartStackData(ChartDataPoint dataPoint, string label)
            : this(new[] {dataPoint}, label)
        {
        }

        public BarChartStackData(IEnumerable<double> values) : this(values, null) { }
        public BarChartStackData(IEnumerable<double> values, string label) : this(values.Select(x => new ChartDataPoint(x)), label) { }

        public BarChartStackData(IEnumerable<ChartDataPoint> dataPoints, string label)
        {
            DataPoints = dataPoints;
            Label = label;
        }

        public IEnumerable<ChartDataPoint> DataPoints { get; private set; }
        public string Label { get; private set; }
    }
}