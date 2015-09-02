using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giger.Charts.BarCharts;
using Giger.Charts.Legends;
using Giger.Nancy;
using Nancy;

namespace Giger.TestSite.Examples.Charts.VerticalBarCharts
{
    public class GridLegends : NancyModule
    {
        public GridLegends()
        {
            Get["/examples/charts/grid-legends/1"] = _ =>
            {
                var svg = new Svg(400, 3 * GridLegend.ItemHeight);

                svg.GridLegend(0, 0)
                    .AddLegendItem(0, 0, "#990000", "Liberal")
                    .AddLegendItem(1, 0, "#000099", "Conservative")
                    .AddLegendItem(0, 1, "#009999", "Bloc Quebecois")
                    .AddLegendItem(1, 1, "#999900", "NDP")
                    .AddLegendItem(2,0,"#999999", "Other");

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/grid-legends/2"] = _ =>
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
                    .WithBottomGutter(3 * GridLegend.ItemHeight + 8);
                chart.GridLegend(chart.X + chart.Width / 2 - GridLegend.ItemWidth, chart.Y + chart.Height - 3 * GridLegend.ItemHeight + 8)
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