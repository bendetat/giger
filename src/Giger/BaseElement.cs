using System.Collections.Generic;
using System.Linq;
using System.Web.Management;
using System.Xml;

namespace Giger
{
    public abstract class BaseElement
    {
        protected readonly IList<BaseElement> Children = new List<BaseElement>();

        protected BaseElement(double? x, double? y, double? width, double? height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        protected bool Removed { get; private set; }

        /// <summary>
        /// Get the X position of this element's origin
        /// </summary>
        public virtual double? X { get; private set; }

        /// <summary>
        /// Get the Y position of this element's origin
        /// </summary>
        public virtual double? Y { get; private set; }
        
        /// <summary>
        /// Get the width of this element
        /// </summary>
        public virtual double? Width { get; private set; }

        /// <summary>
        /// Get the height of this element
        /// </summary>
        public virtual double? Height { get; private set; }

        public abstract IEnumerable<XmlNode> ToXml(XmlDocument doc);

        public T AddChild<T>(T element) where T:BaseElement
        {

            Children.Add(element);

            return element;
        }

        public void AddChildren(IEnumerable<BaseElement> children)
        {
            foreach (var child in children)
            {
                Children.Add(child);
            }
        }

        public void Remove()
        {
            Removed = true;
        }

        protected void SetX(double x)
        {
            X = x;
        }

        protected void SetY(double y)
        {
            Y = y;
        }

        protected void SetWidth(double width)
        {
            Width = width;
        }

        protected void SetHeight(double height)
        {
            Height = height;
        }
    }

    public class ContainerElement : BaseElement
    {
        public ContainerElement() : base(0, 0, 0, 0)
        {
        }

        public ContainerElement(double x, double y, double width, double height) : base(x, y, width, height)
        {
        }

        public override IEnumerable<XmlNode> ToXml(XmlDocument doc)
        {
            return Removed
                ? Enumerable.Empty<XmlNode>()
                : Children.SelectMany(x => x.ToXml(doc));
        }
    }
}