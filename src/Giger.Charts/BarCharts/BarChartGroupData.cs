using System.Collections.Generic;
using System.Linq;

namespace Giger.Charts.BarCharts
{
    public class BarChartGroupData
    {
        public BarChartGroupData(double value) : this(value, null)
        {
        }

        public BarChartGroupData(double value, string label) : this(new ChartDataPoint(value, label))
        {
        }

        public BarChartGroupData(IEnumerable<double> values, string label) : this(values.Select(x => new ChartDataPoint(x)), label)
        {
            
        }

        public BarChartGroupData(ChartDataPoint dataPoint) : this(new[] {dataPoint})
        {
        }

        public BarChartGroupData(IEnumerable<ChartDataPoint> dataPoints) : this(dataPoints.Select(x => new BarChartStackData(x)))
        {
        }

        public BarChartGroupData(IEnumerable<ChartDataPoint> dataPoints, string label) : this(dataPoints.Select(x => new BarChartStackData(x)), label)
        {
        }

        public BarChartGroupData(BarChartStackData stack) : this(stack, null)
        {
        }

        public BarChartGroupData(BarChartStackData stack, string label) : this(new[] {stack}, label)
        {
        }

        public BarChartGroupData(IEnumerable<BarChartStackData> stacks) : this(stacks, null)
        {
        }

        public BarChartGroupData(IEnumerable<BarChartStackData> stacks, string label)
        {
            Stacks = stacks;
            Label = label;
        }

        public IEnumerable<BarChartStackData> Stacks { get; private set; }
        public string Label { get; private set; }
    }
}