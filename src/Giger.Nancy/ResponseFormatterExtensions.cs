using Nancy;

namespace Giger.Nancy
{
    public static class ResponseFormatterExtensions
    {
        public static Response AsSvg(this IResponseFormatter formatter, Svg svg)
        {
            return formatter.AsText(svg.ToString(), "image/svg+xml");
        }
    }
}