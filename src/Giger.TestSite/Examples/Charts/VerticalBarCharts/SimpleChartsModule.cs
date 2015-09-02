using Giger.Charts.BarCharts;
using Giger.Nancy;
using Nancy;

namespace Giger.TestSite.Examples.Charts.VerticalBarCharts
{
    public class SimpleChartsModule : NancyModule
    {
        public SimpleChartsModule()
        {
            Get["/examples/charts/vertical-bar-charts/simple-charts/1"] = _ =>
            {
                var svg = new Svg(200, 150);

                var data = new BarChartData(new double[]
                {
                    50, 25, 5
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .ShowDataLabelOutsideItem();

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/2"] = _ =>
            {
                var svg = new Svg();

                var data = new BarChartData(new[]
                {
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {36, 35, 12}),
                        new BarChartStackData(new double[] {29, 4, 35}),
                        new BarChartStackData(new double[] {19, 11})
                    }),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {18}),
                        new BarChartStackData(new double[] {8, 31, 22}),
                        new BarChartStackData(new double[] {2, 4, 28}),
                    }),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {21, 13, 36}),
                        new BarChartStackData(new double[] {19, 9}),
                        new BarChartStackData(new double[] {34}),
                    }),
                });
                var chart = svg.VerticalBarChart(data);

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/3"] = _ =>
            {
                var svg = new Svg();

                var data = new BarChartData(new[]
                {
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {36, 35, 12}, "Stack 1"),
                        new BarChartStackData(new double[] {29, 4, 35}, "Stack 2"),
                        new BarChartStackData(new double[] {19, 11}, "Stack 3")
                    }, "Group 1"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {18}, "Stack 4"),
                        new BarChartStackData(new double[] {8, 31, 22}, "Stack 5"),
                        new BarChartStackData(new double[] {2, 4, 28}, "Stack 6"),
                    }, "Group 2"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {21, 13, 36}, "Stack 7"),
                        new BarChartStackData(new double[] {19, 9}, "Stack 8"),
                        new BarChartStackData(new double[] {34}, "Stack 9"),
                    }, "Group 3"),
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%");

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/4"] = _ =>
            {
                var svg = new Svg();

                var data = new BarChartData(new double[]
                {
                    50, 25, 5
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .ShowDataLabelOutsideItem()
                    .WithGutter(40)
                    .WithFill("#dddddd")
                    .WithPadding(40)
                    .WithDrawableFill("#cccccc")
                    .WithStroke("black")
                    .WithStrokeWidth(2);

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/5"] = _ =>
            {
                var svg = new Svg(600, 200);

                var data = new BarChartData(new BarChartGroupData[]
                {
                    new BarChartGroupData(new double[]
                    {
                        27, 45
                    }, "Fishing"),
                    new BarChartGroupData(new double[]
                    {
                        0, 15
                    }, "Mining"),
                    new BarChartGroupData(new double[]
                    {
                        0, 10
                    }, "Building"),
                    new BarChartGroupData(new double[]
                    {
                        0, 5
                    }, "Pharmaceutical"),
                    new BarChartGroupData(new double[]
                    {
                        0, 20
                    }, "Technology"),
                    new BarChartGroupData(new double[]
                    {
                        73, 5
                    }, "Farming"),
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .ShowDataLabelOutsideItem()
                    .WithPointColorGenerator(new StackBasedDataPointColorGenerator(new[]
                    {
                        "#6666ff",
                        "orange"
                    }))
                    .WithStroke("black")
                    .WithStrokeWidth(2)
                    .WithPadding(10);

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/6"] = _ =>
            {
                var svg = new Svg(600, 200);

                var data = new BarChartData(new BarChartGroupData[]
                {
                    new BarChartGroupData(new double[]
                    {
                        27, 45
                    }, "Fishing"),
                    new BarChartGroupData(new double[]
                    {
                        0, 15
                    }, "Mining"),
                    new BarChartGroupData(new double[]
                    {
                        0, 10
                    }, "Building"),
                    new BarChartGroupData(new double[]
                    {
                        0, 5
                    }, "Pharmaceutical"),
                    new BarChartGroupData(new double[]
                    {
                        0, 20
                    }, "Technology"),
                    new BarChartGroupData(new double[]
                    {
                        73, 5
                    }, "Farming"),
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .AlwaysShowDataLabel()
                    .WithPointColorGenerator(new StackBasedDataPointColorGenerator(new[]
                    {
                        "#6666ff",
                        "orange"
                    }))
                    .WithStroke("black")
                    .WithStrokeWidth(2)
                    .WithPadding(10);

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/7"] = _ =>
            {
                var svg = new Svg();

                var data = new BarChartData(new[]
                {
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {36, 35, 12}, "Stack 1"),
                        new BarChartStackData(new double[] {29, 4, 35}, "Stack 2"),
                        new BarChartStackData(new double[] {19, 11}, "Stack 3")
                    }, "Group 1"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {18}, "Stack 4"),
                        new BarChartStackData(new double[] {8, 31, 22}, "Stack 5"),
                        new BarChartStackData(new double[] {2, 4, 28}, "Stack 6"),
                    }, "Group 2"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {21, 13, 36}, "Stack 7"),
                        new BarChartStackData(new double[] {19, 9}, "Stack 8"),
                        new BarChartStackData(new double[] {34}, "Stack 9"),
                    }, "Group 3"),
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .WithPointColorGenerator(new PointBasedDataPointColorGenerator(new[]
                    {
                        "#617073",
                        "#7A93AC",
                        "#92BCEA",
                        "#AFB3F7"
                    }));

                return Response.AsSvg(svg);
            };

            Get["/examples/charts/vertical-bar-charts/simple-charts/8"] = _ =>
            {
                var svg = new Svg();

                var data = new BarChartData(new[]
                {
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {36, 35, 12}, "Stack 1"),
                        new BarChartStackData(new double[] {29, 4, 35}, "Stack 2"),
                        new BarChartStackData(new double[] {19, 11}, "Stack 3")
                    }, "Group 1"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {18}, "Stack 4"),
                        new BarChartStackData(new double[] {8, 31, 22}, "Stack 5"),
                        new BarChartStackData(new double[] {2, 4, 28}, "Stack 6"),
                    }, "Group 2"),
                    new BarChartGroupData(new[]
                    {
                        new BarChartStackData(new double[] {21, 13, 36}, "Stack 7"),
                        new BarChartStackData(new double[] {19, 9}, "Stack 8"),
                        new BarChartStackData(new double[] {34}, "Stack 9"),
                    }, "Group 3"),
                });
                var chart = svg.VerticalBarChart(data)
                    .WithDataLabelFormat("{0}%")
                    .WithPointColorGenerator(new PointBasedDataPointColorGenerator(new[]
                    {
                        "#171A21",
                        "#617073",
                        "#7A93AC",
                        "#92BCEA",
                        "#AFB3F7"
                    }))
                    .WithPointLabelColorGenerator(new PointBasedDataPointColorGenerator(new[]
                    {
                        "#fff",
                        "#fff",
                        "#000",
                        "#000",
                        "#000",
                    }));

                return Response.AsSvg(svg);
            };
        }
    }
}