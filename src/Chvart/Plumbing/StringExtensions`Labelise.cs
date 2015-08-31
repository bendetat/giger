using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chvart.Plumbing
{
    public static partial class StringExtensions
    {
        public static string Labelise(this string label, double val, double total)
        {
            if (string.IsNullOrEmpty(label) || !label.Contains("."))
            {
                return val.ToString(CultureInfo.InvariantCulture);
            }

            var precision = label
                .Substring(label.IndexOf("."))
                .Replace("%", "")
                .Length - 1;

            return label.Contains("%") 
                ? (val/total).ToString($"p{precision}") 
                : val.ToString($"f{precision}");
        }
    }
}
