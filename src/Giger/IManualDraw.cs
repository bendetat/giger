namespace Giger
{
    /// <summary>
    /// Elements that implement IManualDraw have their draw method called before the
    /// element is rendered to SVG. This is used for complex elements that need to be
    /// set up prior to rendering, such as bar charts, where the child elements cannot
    /// be added until after the element has been fully set up. This is to avoid
    /// the caller from having to call Draw() directly, as if the Draw call is missed
    /// the element won't be rendered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManualDraw<out T> where T : BaseElement
    {
        /// <summary>
        /// The implementation of Draw() should be idempotent, aside from assuming
        /// that there are no existing children. The children of the element are 
        /// cleared before calling Draw(), so if it happens to be called by the
        /// consuming code it won't double up the children when called again.
        /// </summary>
        /// <returns>this</returns>
        T Draw();
    }
}