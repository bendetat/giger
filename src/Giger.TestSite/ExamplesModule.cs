using Nancy;

namespace Giger.TestSite
{
    public class ExamplesModule : NancyModule
    {
        public ExamplesModule()
        {
            Get["/examples/barchart.html"] = _ => View["barchart.html"];
            Get["/examples/barchart.svg"] = _ =>
            {
                var chvart = new Svg();

                var txtAttr = new {font = "12px 'Fontin Sans', Fontin-Sans, sans-serif"};
                chvart
                    .Text(160, 10, "Single Series Chart")
                    .SetAttr(txtAttr);
                chvart
                    .Text(480, 10, "Multiline Series Chart")
                    .SetAttr(txtAttr);
                chvart
                    .Text(160, 250, "Multiple Series Stacked Chart")
                    .SetAttr(txtAttr);
                chvart
                    .Text(480, 250, "Multiline Series Stacked Vertical Chart. Type \"round\"")
                    .SetAttr(txtAttr);

                //chvart
                //    .VerticalBarChart(10, 10, 300, 220,
                //        new double[] {55, 20, 13, 32, 5, 1, 2, 10}, 
                //        new Chvart.VerticalBarChart.Options {Type = Ending.Sharp});
                //chvart
                //	.BarChart(330, 10, 300, 220, data1);
                //chvart
                //	.BarChart(10, 250, 300, 220, data2, { stacked: true});
                //chvart
                //	.BarChart(330, 250, 300, 220, data3, { stacked: true, type: "round"});

                return chvart.ToString();
            };
        }
    }
}