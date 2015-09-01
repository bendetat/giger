using System;

namespace Giger.Gradients
{

    public abstract class Gradient<T> : Element<T> where T : Gradient<T>
    {
        protected Gradient() : base(0, 0, 0, 0)
        {
            SetAttr(new {id = Guid.NewGuid().ToString()});
        }

        public string Id => GetAttrOrDefault("id", null);

        public T AddStop(string offset, string stopColor, double stopOpacity = 1)
        {
            AddChild(new GradientStop(offset, stopColor, stopOpacity));

            return this as T;
        }
    }
}