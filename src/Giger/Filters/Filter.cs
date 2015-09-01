using System;
using System.Xml;
using Giger.Plumbing;
using Humanizer;

namespace Giger.Filters
{
    public class Filter : Element<Filter>
    {
        public Filter(double? x, double? y, double? width, double? height) : base(x, y, width, height)
        {
            SetAttr("id", Guid.NewGuid().ToString());
        }

        public string Id => GetAttrOrDefault("id", null);

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviation)
        {
            return this.AddChild(new GaussianBlurFilterEffect(standardDeviation, standardDeviation, null, null));
        }

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviationX, double standardDeviationY)
        {
            return AddChild(new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, null, null));
        }

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviation, FilterInput filterInput, string resultName = null)
        {
            return AddChild(new GaussianBlurFilterEffect(standardDeviation, standardDeviation, filterInput.ToString(), resultName));
        }

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviationX, double standardDeviationY, FilterInput filterInput, string resultName = null)
        {
            return AddChild(new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, filterInput.ToString(), resultName));
        }

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviation, string filterInput, string resultName = null)
        {
            return AddChild(new GaussianBlurFilterEffect(standardDeviation, standardDeviation, filterInput, resultName));
        }

        public GaussianBlurFilterEffect GaussianBlur(double standardDeviationX, double standardDeviationY, string filterInput, string resultName = null)
        {
            return AddChild(new GaussianBlurFilterEffect(standardDeviationX, standardDeviationY, filterInput, resultName));
        }

        public OffsetFilterEffect Offset(double dx, double dy)
        {
            return AddChild(new OffsetFilterEffect(dx, dy, null, null));
        }

        public OffsetFilterEffect Offset(double dx, double dy, FilterInput? filterInput, string resultName = null)
        {
            return AddChild(new OffsetFilterEffect(dx, dy, filterInput.ToString(), resultName));
        }

        public OffsetFilterEffect Offset(double dx, double dy, string filterInput, string resultName = null)
        {
            return AddChild(new OffsetFilterEffect(dx, dy, filterInput, resultName));
        }

        public BlendFilterEffect Blend(FilterInput filterInput, FilterInput filterInput2, BlendMode mode, string resultName = null)
        {
            return AddChild(new BlendFilterEffect(filterInput.ToString(), filterInput2.ToString(), mode, resultName));
        }

        public BlendFilterEffect Blend(string filterInput, FilterInput filterInput2, BlendMode mode, string resultName = null)
        {
            return AddChild(new BlendFilterEffect(filterInput, filterInput2.ToString(), mode, resultName));
        }

        public BlendFilterEffect Blend(FilterInput filterInput, string filterInput2, BlendMode mode, string resultName = null)
        {
            return AddChild(new BlendFilterEffect(filterInput.ToString(), filterInput2, mode, resultName));
        }

        public BlendFilterEffect Blend(string filterInput, string filterInput2, BlendMode mode, string resultName = null)
        {
            return AddChild(new BlendFilterEffect(filterInput, filterInput2, mode, resultName));
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("filter");
        }

        public ColorMatrixFilterEffect ColorMatrix(double[] matrix)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, null, null));
        }

        public ColorMatrixFilterEffect ColorMatrix(string matrix)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, null, null));
        }

        public ColorMatrixFilterEffect ColorMatrix(double[] matrix, FilterInput filterInput, string resultName = null)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, filterInput.ToString(), resultName));
        }

        public ColorMatrixFilterEffect ColorMatrix(string matrix, FilterInput filterInput, string resultName = null)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, filterInput.ToString(), resultName));
        }

        public ColorMatrixFilterEffect ColorMatrix(double[] matrix, string filterInput, string resultName = null)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, filterInput, resultName));
        }

        public ColorMatrixFilterEffect ColorMatrix(string matrix, string filterInput, string resultName = null)
        {
            return AddChild(new ColorMatrixFilterEffect(matrix, filterInput, resultName));
        }

        public Filter WithFilterUnits(FilterUnits filterUnits)
        {
            return SetAttr("filterUnits", filterUnits.ToString().Camelize());
        }

        public SpecularLightingFilterEffect SpecularLighting(
            double surfaceScale,
            double specularConstant,
            double specularExponent,
            string lightingColor,
            string filterInputName = null,
            string resultName = null)
        {
            return this.AddChild(new SpecularLightingFilterEffect(
                surfaceScale,
                specularConstant,
                specularExponent,
                lightingColor,
                filterInputName,
                resultName));
        }

        public CompositeFilterEffect Composite(FilterInput filterInput1, FilterInput filterInput2, CompositeOperator @operator, string resultName = null)
        {
            return AddChild(new CompositeFilterEffect(filterInput1.ToString(), filterInput2.ToString(), @operator, resultName));
        }

        public CompositeFilterEffect Composite(string filterInput1, FilterInput filterInput2, CompositeOperator @operator, string resultName = null)
        {
            return AddChild(new CompositeFilterEffect(filterInput1, filterInput2.ToString(), @operator, resultName));
        }

        public CompositeFilterEffect Composite(FilterInput filterInput1, string filterInput2, CompositeOperator @operator, string resultName = null)
        {
            return AddChild(new CompositeFilterEffect(filterInput1.ToString(), filterInput2, @operator, resultName));
        }

        public CompositeFilterEffect Composite(string filterInput1, string filterInput2, CompositeOperator @operator, string resultName = null)
        {
            return AddChild(new CompositeFilterEffect(filterInput1, filterInput2, @operator, resultName));
        }
    }
}