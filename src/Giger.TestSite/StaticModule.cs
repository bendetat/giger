using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Giger.TestSite
{
    public class StaticModule : NancyModule
    {
        public StaticModule()
        {
            Get["/"] = _ => Response.AsFile("site/index.html");
        }
    }
}