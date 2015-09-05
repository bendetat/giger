using Giger.Charts.BarCharts;
using Giger.Charts.ColorGenerators;
using Giger.Nancy;
using Nancy;

namespace Giger.TestSite.Examples.Charts.VerticalBarCharts
{
    public class VerticalChartLabelsModule : NancyModule
    {
        public VerticalChartLabelsModule()
        {
            Get["/examples/charts/vertical-bar-charts/vertical-chart-labels/multi-line-group-labels"] = _ =>
            {
                var svg = new Svg(400, 200);

                var chart = svg.VerticalBarChart(DataSources.GetIndustriesData())
                    .WithDataLabelFormat("{0}%")
                    .ShowDataLabelOutsideItem()
                    .WithPointColorGenerator(new StackBasedDataPointColorGenerator(new[]
                    {
                        "#6666ff",
                        "orange"
                    }))
                    .WithStroke("black")
                    .WithStrokeWidth(2)
                    .WithPadding(10)
                    .WithGroupLabelAutoLineSplit(8);

                return Response.AsSvg(svg);
            };
        }
    }
}