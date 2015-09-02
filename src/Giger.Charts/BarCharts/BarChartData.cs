using System.Collections.Generic;
using System.Linq;

namespace Giger.Charts.BarCharts
{
    public class BarChartData
    {
        public BarChartData(IEnumerable<double> values) : this(values.Select(x => new ChartDataPoint(x)))
        {
        }

        public BarChartData(IEnumerable<ChartDataPoint> dataPoints) : this(dataPoints.Select(x => new BarChartStackData(x)))
        {
        }

        public BarChartData(IEnumerable<BarChartStackData> stacks) : this(stacks.Select(x => new BarChartGroupData(x)))
        {
        }

        public BarChartData(IEnumerable<BarChartGroupData> groups)
        {
            Groups = groups;
        }

        public IEnumerable<BarChartGroupData> Groups { get; set; }
    }
}