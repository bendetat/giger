using Giger.Charts.BarCharts;
using Giger.Charts.ColorGenerators;
using Giger.Charts.Legends;
using Giger.Nancy;
using Nancy;

namespace Giger.TestSite.Examples.Charts.VerticalBarCharts
{
    public class GridLegendsModule : NancyModule
    {
        public GridLegendsModule()
        {
            Get["/examples/charts/grid-legends/standalone-legend"] = _ =>
            {
                var svg = new Svg(400, 3*GridLegend.ItemHeight);

                svg.GridLegend(0, 0)
                    .AddLegendItem(0, 0, "#990000", "Liberal")
                    .AddLegendItem(1, 0, "#000099", "Conservative")
                    .AddLegendItem(0, 1, "#009999", "Bloc Quebecois")
                    .AddLegendItem(1, 1, "#999900", "NDP")
                    .AddLegendItem(2, 0, "#999999", "Other");

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/grid-legends/legend-under-chart"] = _ =>
            {
                var svg = new Svg(400, 400);

                var data = new BarChartData(new double[]
                {
                    133, 98, 54, 19, 4
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}")
                    .AlwaysShowDataLabel()
                    .ShowDataLabelOutsideItem()
                    .WithPointColorGenerator(new GroupBasedDataPointColorGenerator(new[]
                    {
                        "#990000", "#000099", "#009999", "#999900", "#999999"
                    }))
                    // bottom gutter for legend
                    .WithBottomGutter(3*GridLegend.ItemHeight + 8);
                chart.GridLegend(chart.X ?? 0 + (chart.Width ?? 0)/2 - GridLegend.ItemWidth, chart.Y ?? 0 + chart.Height ?? 0 - 3*GridLegend.ItemHeight + 8)
                    .AddLegendItem(0, 0, "#990000", "Liberal")
                    .AddLegendItem(1, 0, "#000099", "Conservative")
                    .AddLegendItem(0, 1, "#009999", "Bloc Quebecois")
                    .AddLegendItem(1, 1, "#999900", "NDP")
                    .AddLegendItem(2, 0, "#999999", "Other");

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/grid-legends/legend-embedded-in-chart"] = _ =>
            {
                var svg = new Svg(400, 200)
                    .WithBackgroundFill("#eee");

                var data = new BarChartData(new double[]
                {
                    133, 98, 54, 19, 4
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}")
                    .AlwaysShowDataLabel()
                    .ShowDataLabelOutsideItem()
                    .WithPointColorGenerator(new GroupBasedDataPointColorGenerator(new[]
                    {
                        "#990000", "#000099", "#009999", "#999900", "#999999"
                    }))
                    .WithGutter(20);
                chart.GridLegend(chart.Width ?? 0 - GridLegend.ItemWidth*2 - 10, 10)
                    .AddLegendItem(0, 0, "#990000", "Liberal")
                    .AddLegendItem(1, 0, "#000099", "Conservative")
                    .AddLegendItem(0, 1, "#009999", "Bloc Quebecois")
                    .AddLegendItem(1, 1, "#999900", "NDP")
                    .AddLegendItem(2, 0, "#999999", "Other");

                return Response.AsSvg(svg);
            };
        }
    }
}