using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Chvart.Raphael;
using Nancy;

namespace Chvart.TestSite
{
    public class PagesModule : NancyModule
    {
        public PagesModule()
        {
            Get["/examples/barchart.html"] = _ => View["barchart.html"];
            Get["/examples/barchart.svg"] = _ =>
            {
                var chvart = new Chvart();

                var txtAttr = new {font =  "12px 'Fontin Sans', Fontin-Sans, sans-serif"};
                chvart
                    .Text(160, 10, "Single Series Chart")
                    .Attr(txtAttr);
                chvart
                    .Text(480, 10, "Multiline Series Chart")
                    .Attr(txtAttr);
                chvart
                    .Text(160, 250, "Multiple Series Stacked Chart")
                    .Attr(txtAttr);
                chvart
                    .Text(480, 250, "Multiline Series Stacked Vertical Chart. Type \"round\"")
                    .Attr(txtAttr);

                //chvart
                //	.BarChart(10, 10, 300, 220, [[55, 20, 13, 32, 5, 1, 2, 10]], 0, { type: "sharp"});
                //chvart
                //	.BarChart(330, 10, 300, 220, data1);
                //chvart
                //	.BarChart(10, 250, 300, 220, data2, { stacked: true});
                //chvart
                //	.BarChart(330, 250, 300, 220, data3, { stacked: true, type: "round"});

                return chvart.ToSvg();
            };
        }
    }
}