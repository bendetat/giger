using System;

namespace Giger.Filters
{
    public abstract class FilterEffect<T> : Element<T> where T: BaseElement
    {
        protected FilterEffect(string filterInputName, string resultName)
        {
            if (!string.IsNullOrEmpty(filterInputName))
            {
                SetAttr("in", filterInputName);
            }

            if (!string.IsNullOrEmpty(resultName))
            {
                SetAttr("result", resultName ?? Guid.NewGuid().ToString());
            }
        }
    }
}