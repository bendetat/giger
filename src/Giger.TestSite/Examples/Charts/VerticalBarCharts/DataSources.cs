using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giger.Charts;
using Giger.Charts.BarCharts;

namespace Giger.TestSite.Examples.Charts.VerticalBarCharts
{
    public static class DataSources
    {
        public static BarChartData GetRenewableEnergyData()
        {
            var data = new ChartDataPoint[]
            {
                new ChartDataPoint(0.0224, "Solar hot water"),
                new ChartDataPoint(0.1497, "Hydroelectricity"),
                new ChartDataPoint(0.3310, "Bioenergy - wood and woodwaste"),
                new ChartDataPoint(0.0600, "Bioenergy - biogas and biofuels"),
                new ChartDataPoint(0.3859, "Bioenergy - bagasse"),
                new ChartDataPoint(0.0490, "Wind"),
                new ChartDataPoint(0.0014, "Solar - solar electricity"),
            };
            return new BarChartData(data);
        }

        public static BarChartData GetIndustriesData()
        {
            return new BarChartData(new BarChartGroupData[]
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
        }

    }
}