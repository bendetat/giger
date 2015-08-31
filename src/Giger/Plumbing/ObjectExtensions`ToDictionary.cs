using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Giger.Plumbing
{
    internal static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            return obj == null
                ? new Dictionary<string, object>()
                : TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>()
                    .ToDictionary(
                        property => property.Name,
                        property => property.GetValue(obj));
        }
    }
}