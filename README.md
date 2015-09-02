# Giger

A C# library for generating SVG images.


## SVG Specification implementation state

The intention is to implement as much of the [SVG 1.1 Specification][svg-1.1-spec] (and any future versions) as possible, apart from features that are out of scope.


### Out of scope

These features are out of scope for the forseeable future, however if anybody wants to add some of these features you are most welcome.

- [Styling][svg-1.1-spec-styling] via CSS either by classes or styles is out of scope at the moment (but could be considered later) - need to decide how to share the API between inline styles and CSS styles. All inline styles are in scope.
- [Interactivity][svg-1.1-spec-interactivity]
	- [pointer-events](http://www.w3.org/TR/SVG11/interact.html#PointerEventsProperty)
- [Scripting][svg-1.1-spec-scripting]
- [Animation][svg-1.1-spec-animation]
- [Metadata][svg-1.1-spec-metadata]
- [Extensibility][svg-1.1-spec-extensibility]


### Styling

- [Font properties](http://www.w3.org/TR/SVG11/styling.html#FontPropertiesUsedBySVG)
	- [ ] [font](http://www.w3.org/TR/SVG11/styling.html#FontProperty)
	- [x] [font-family](http://www.w3.org/TR/SVG11/styling.html#FontFamilyProperty)
	- [x] [font-size](http://www.w3.org/TR/SVG11/styling.html#FontSizeProperty)
	- [x] [font-size-adjust](http://www.w3.org/TR/SVG11/styling.html#FontSizeAdjustProperty)
	- [x] [font-stretch](http://www.w3.org/TR/SVG11/styling.html#FontStretchProperty)
	- [x] [font-style](http://www.w3.org/TR/SVG11/styling.html#FontStyleProperty)
	- [x] [font-variant](http://www.w3.org/TR/SVG11/styling.html#FontVariantProperty)
	- [x] [font-weight](http://www.w3.org/TR/SVG11/styling.html#FontWeightProperty)
- Text properties: 
	- [ ] [direction](http://www.w3.org/TR/SVG11/styling.html#DirectionProperty)
	- [ ] [letter-spacing](http://www.w3.org/TR/SVG11/styling.html#LetterSpacingProperty)
	- [ ] [text-decoration](http://www.w3.org/TR/SVG11/styling.html#TextDecorationProperty)
	- [ ] [unicode-bidi](http://www.w3.org/TR/SVG11/styling.html#UnicodeBidiProperty)
	- [ ] [word-spacing](http://www.w3.org/TR/SVG11/styling.html#WordSpacingProperty)
- Other properties for visual media: 
	- [ ] [clip](http://www.w3.org/TR/SVG11/masking.html#ClipProperty)
	- [ ] [color](http://www.w3.org/TR/SVG11/color.html#ColorProperty)
	- [ ] [cursor](http://www.w3.org/TR/SVG11/interact.html#CursorProperty)
	- [ ] [display](http://www.w3.org/TR/SVG11/painting.html#DisplayProperty)
	- [ ] [overflow](http://www.w3.org/TR/SVG11/masking.html#OverflowProperty)
	- [ ] [visibility](http://www.w3.org/TR/SVG11/painting.html#VisibilityProperty)
- [Clipping, Masking and Compositing](http://www.w3.org/TR/SVG11/masking.html)
	- [ ] [clip-path](http://www.w3.org/TR/SVG11/masking.html#ClipPathProperty)
	- [ ] [clip-rule](http://www.w3.org/TR/SVG11/masking.html#ClipRuleProperty)
	- [ ] [mask](http://www.w3.org/TR/SVG11/masking.html#MaskProperty)
	- [ ] [opacity](http://www.w3.org/TR/SVG11/masking.html#OpacityProperty)
- [Filter Effects](http://www.w3.org/TR/SVG11/filters.html)
	- [ ] [enable-background](http://www.w3.org/TR/SVG11/filters.html#EnableBackgroundProperty)
	- [x] [filter](http://www.w3.org/TR/SVG11/filters.html#FilterProperty)
	- [ ] [flood-color](http://www.w3.org/TR/SVG11/filters.html#FloodColorProperty)
	- [ ] [flood-opacity](http://www.w3.org/TR/SVG11/filters.html#FloodOpacityProperty)
	- [ ] [lighting-color](http://www.w3.org/TR/SVG11/filters.html#LightingColorProperty)
- [Gradient](http://www.w3.org/TR/SVG11/pservers.html#Gradients)
	- [x] [stop-color](http://www.w3.org/TR/SVG11/pservers.html#StopColorProperty)
	- [x] [stop-opacity](http://www.w3.org/TR/SVG11/pservers.html#StopOpacityProperty)
- [Color](http://www.w3.org/TR/SVG11/color.html) and [Painting](http://www.w3.org/TR/SVG11/painting.html)
	- [ ] [color-interpolation](http://www.w3.org/TR/SVG11/painting.html#ColorInterpolationProperty)
	- [ ] [color-interpolation-filters](http://www.w3.org/TR/SVG11/painting.html#ColorInterpolationFiltersProperty)
	- [ ] [color-profile](http://www.w3.org/TR/SVG11/color.html#ColorProfileProperty)
	- [ ] [color-rendering](http://www.w3.org/TR/SVG11/painting.html#ColorRenderingProperty)
	- [x] [fill](http://www.w3.org/TR/SVG11/painting.html#FillProperty)
	- [ ] [fill-opacity](http://www.w3.org/TR/SVG11/painting.html#FillOpacityProperty)
	- [ ] [fill-rule](http://www.w3.org/TR/SVG11/painting.html#FillRuleProperty)
	- [ ] [image-rendering](http://www.w3.org/TR/SVG11/painting.html#ImageRenderingProperty)
	- [ ] [marker](http://www.w3.org/TR/SVG11/painting.html#MarkerProperty)
	- [ ] [marker-end](http://www.w3.org/TR/SVG11/painting.html#MarkerEndProperty)
	- [ ] [marker-mid](http://www.w3.org/TR/SVG11/painting.html#MarkerMidProperty)
	- [ ] [marker-start](http://www.w3.org/TR/SVG11/painting.html#MarkerStartProperty)
	- [ ] [shape-rendering](http://www.w3.org/TR/SVG11/painting.html#ShapeRenderingProperty)
	- [x] [stroke](http://www.w3.org/TR/SVG11/painting.html#StrokeProperty)
	- [x] [stroke-dasharray](http://www.w3.org/TR/SVG11/painting.html#StrokeDasharrayProperty)
	- [ ] [stroke-dashoffset](http://www.w3.org/TR/SVG11/painting.html#StrokeDashoffsetProperty)
	- [x] [stroke-linecap](http://www.w3.org/TR/SVG11/painting.html#StrokeLinecapProperty)
	- [ ] [stroke-linejoin](http://www.w3.org/TR/SVG11/painting.html#StrokeLinejoinProperty)
	- [ ] [stroke-miterlimit](http://www.w3.org/TR/SVG11/painting.html#StrokeMiterlimitProperty)
	- [x] [stroke-opacity](http://www.w3.org/TR/SVG11/painting.html#StrokeOpacityProperty)
	- [x] [stroke-width](http://www.w3.org/TR/SVG11/painting.html#StrokeWidthProperty)
	- [ ] [text-rendering](http://www.w3.org/TR/SVG11/painting.html#TextRenderingProperty)
- [Text](http://www.w3.org/TR/SVG11/text.html)
	- [ ] [alignment-baseline](http://www.w3.org/TR/SVG11/text.html#AlignmentBaselineProperty)
	- [ ] [baseline-shift](http://www.w3.org/TR/SVG11/text.html#BaselineShiftProperty)
	- [ ] [dominant-baseline](http://www.w3.org/TR/SVG11/text.html#DominantBaselineProperty)
	- [ ] [glyph-orientation-horizontal](http://www.w3.org/TR/SVG11/text.html#GlyphOrientationHorizontalProperty)
	- [ ] [glyph-orientation-vertical](http://www.w3.org/TR/SVG11/text.html#GlyphOrientationVerticalProperty)
	- [ ] [kerning](http://www.w3.org/TR/SVG11/text.html#KerningProperty)
	- [ ] [text-anchor](http://www.w3.org/TR/SVG11/text.html#TextAnchorProperty)
	- [ ] [writing-mode](http://www.w3.org/TR/SVG11/text.html#WritingModeProperty)


### [Coordinate Systems, Transformations and Units](http://www.w3.org/TR/SVG11/coords.html)

- The [transform](http://www.w3.org/TR/SVG11/coords.html#TransformAttribute) attribute
	- [ ] matrix
	- [ ] translate
	- [ ] scale
	- [x] rotate
	- [ ] skewX
	- [ ] skewY
- [ ] The [viewBox](http://www.w3.org/TR/SVG11/coords.html#ViewBoxAttribute) attribute      
- [ ] The [preserveAspectRatio](http://www.w3.org/TR/SVG11/coords.html#PreserveAspectRatioAttribute) attribute


### [Paths](http://www.w3.org/TR/SVG11/paths.html)

- [x] moveto - M
- [x] moveto - m
- [x] closepath - Z
- [x] closepath - z
- [x] lineto - L
- [x] lineto - l
- [ ] horizontal lineto - H
- [ ] horizontal lineto - h
- [ ] vertical lineto - V
- [ ] vertical lineto - v
- [ ] curveto - C
- [ ] curveto - c
- [ ] curveto - S
- [ ] curveto - s
- [x] quadratic bezier curveto - Q
- [x] quadratic bezier curveto - q
- [ ] quadratic bezier curveto - T
- [ ] quadratic bezier curveto - t
- [ ] elliptical arc - A
- [ ] elliptical arc - a


### [Basic shapes](http://www.w3.org/TR/SVG11/shapes.html)

- [x] [rect](http://www.w3.org/TR/SVG11/shapes.html#RectElement)
	- [x] x
	- [x] y
	- [x] width
	- [x] height
	- [x] rx
	- [x] ry
- [x] [circle](http://www.w3.org/TR/SVG11/shapes.html#CircleElement)
	- [x] cx
	- [x] cy
	- [x] r
- [x] [ellipse](http://www.w3.org/TR/SVG11/shapes.html#EllipseElement)
	- [x] cx
	- [x] cy
	- [x] rx
	- [x] ry
- [x] [line](http://www.w3.org/TR/SVG11/shapes.html#LineElement)
	- [x] x1
	- [x] y1
	- [x] x2
	- [x] y2
- [x] [polyline](http://www.w3.org/TR/SVG11/shapes.html#PolylineElement)
	- [x] points
- [x] [polygon](http://www.w3.org/TR/SVG11/shapes.html#PolygonElement)
	- [x] points

### [Text](http://www.w3.org/TR/SVG11/text.html)

- [ ] [text](http://www.w3.org/TR/SVG11/text.html#TextElement) 
	- [x] x
	- [x] y
	- [ ] dx
	- [ ] dy
	- [ ] rotate
	- [ ] textLength
	- [ ] lengthAdjust
- [ ] [tspan](http://www.w3.org/TR/SVG11/text.html#TSpanElement)
	- [x] x - single coordinate
	- [ ] x - list of coordinates
	- [x] y - single coordinate
	- [ ] y - list of coordinates
	- [ ] dx
	- [ ] dy
	- [ ] rotate
	- [ ] textLength
- [ ] [tref](http://www.w3.org/TR/SVG11/text.html#TRefElement)
	- [ ] xlink:href
- [ ] [altGlyph](http://www.w3.org/TR/SVG11/text.html#AltGlyphElement)
	- [ ] xlink:href
	- [ ] glyphRef
	- [ ] format
	- [ ] x
	- [ ] y
	- [ ] dx
	- [ ] dy
	- [ ] rotate
- [ ] [altGlyphDef](http://www.w3.org/TR/SVG11/text.html#AltGlyphDefElement)
- [ ] [altGlyphItem](http://www.w3.org/TR/SVG11/text.html#AltGlyphItemElement)
- [ ] [glyphRef](http://www.w3.org/TR/SVG11/text.html#GlyphRefElement)
- [ ] [xml:space](http://www.w3.org/TR/SVG11/struct.html#XMLSpaceAttribute) (white space handling)


### [Gradients and Patterns](http://www.w3.org/TR/SVG11/pservers.html)

- [x] [linearGradient](http://www.w3.org/TR/SVG11/pservers.html#LinearGradientElement)
	- [x] x1
	- [x] y1
	- [x] x2
	- [x] y2
	- [x] gradientUnits
	- [ ] gradientTransform
	- [ ] spreadMethod
	- [ ] xlink:href
- [x] [radialGradient](http://www.w3.org/TR/SVG11/pservers.html#RadialGradientElement)
	- [x] gradientUnits
	- [ ] gradientTransform
	- [x] cx
	- [x] cy
	- [x] r
	- [x] fx
	- [x] fy
	- [ ] spreadMethod
	- [ ] xlink:href
- [x] [stop](http://www.w3.org/TR/SVG11/pservers.html#StopElement) - gradient stops
	- [x] offset
	- [x] stop-color
	- [x] stop-opacity
- [ ] [Patterns](http://www.w3.org/TR/SVG11/pservers.html#PatternElement)
	- [ ] patternUnits
	- [ ] patternContentUnits
	- [ ] patternTransform
	- [ ] x
	- [ ] y
	- [ ] width
	- [ ] height
	- [ ] xlink:href
	- [ ] preserveAspectRatio

### [Document Structure](http://www.w3.org/TR/SVG11/struct.html)

- [ ] [svg](http://www.w3.org/TR/SVG11/struct.html#SVGElement)
	- [ ] version
	- [ ] baseProfile
	- [ ] x
	- [ ] y
	- [x] width
	- [x] height
	- [ ] preserveAspectRatio
	- [ ] contentScriptType
	- [ ] contentStyleType
	- [ ] zoomAndPan
- [x] [g](http://www.w3.org/TR/SVG11/struct.html#GElement) - grouping
- [x] [defs](http://www.w3.org/TR/SVG11/struct.html#DefsElement)
- [ ] [desc](http://www.w3.org/TR/SVG11/struct.html#DescElement)
- [ ] [title](http://www.w3.org/TR/SVG11/struct.html#TitleElement)
- [ ] [symbol](http://www.w3.org/TR/SVG11/struct.html#SymbolElement)
- [ ] [use](http://www.w3.org/TR/SVG11/struct.html#UseElement)
- [ ] [image](http://www.w3.org/TR/SVG11/struct.html#ImageElement)
- [ ] [switch](http://www.w3.org/TR/SVG11/struct.html#SwitchElement)
	- [ ] requiredFeatures
	- [ ] requiredExtensions
	- [ ] systemLanguage

### [Clipping, Masking and Compositing](http://www.w3.org/TR/SVG11/masking.html)

- [ ] overflow
- [ ] clip
- [ ] mask

### [Filter effects](http://www.w3.org/TR/SVG11/filters.html)

- [filter](http://www.w3.org/TR/SVG11/filters.html#FilterElement) element
	- [x] filterUnits
	- [ ] primitiveUnits
	- [x] x
	- [x] y
	- [x] width
	- [x] height
	- [ ] filterRes
	- [ ] xlink:href
- [ ] [filter](http://www.w3.org/TR/SVG11/filters.html#FilterProperty) property
- [ ] [enable-background](http://www.w3.org/TR/SVG11/filters.html#EnableBackgroundProperty) property

#### Filter primitives

Common attributes:

- [x] x
- [x] y
- [x] width
- [x] height
- [x] result
- [x] in

Light source elements and properties:

- [ ] [feDistantLight](http://www.w3.org/TR/SVG11/filters.html#feDistantLightElement)
	- [ ] azimuth
	- [ ] elevation
- [x] [fePointLight](http://www.w3.org/TR/SVG11/filters.html#fePointLightElement)
	- [x] x
	- [x] y
	- [x] z
- [ ] [feSpotLight](http://www.w3.org/TR/SVG11/filters.html#feSpotLightElement)
	- [ ] x
	- [ ] y
	- [ ] z
	- [ ] pointsAtX
	- [ ] pointsAtY
	- [ ] pointsAtZ
	- [ ] specularExponent
	- [ ] limitingConeAngle
- [ ] [lighting-color](http://www.w3.org/TR/SVG11/filters.html#LightingColorProperty) property

Primitives:

- [x] [feBlend](http://www.w3.org/TR/SVG11/filters.html#feBlendElement)
	- [x] mode
	- [x] in2
- [x] [feColorMatrix](http://www.w3.org/TR/SVG11/filters.html#feColorMatrix)
	- [ ] type
	- [x] values
- [ ] [feComponentTransfer](http://www.w3.org/TR/SVG11/filters.html#feComponentTransfer)
	- [ ] feFuncR
	- [ ] feFuncG
	- [ ] feFuncB
	- [ ] feFuncA
- [x] [feComposite](http://www.w3.org/TR/SVG11/filters.html#feComposite)
	- [x] operator
	- [ ] k1
	- [ ] k2
	- [ ] k3
	- [ ] k4
	- [x] in2
- [ ] [feConvolveMatrix](http://www.w3.org/TR/SVG11/filters.html#feConvolveMatrix)
	- [ ] order
	- [ ] kernelMatrix
	- [ ] divisor
	- [ ] bias
	- [ ] targetX
	- [ ] targetY
	- [ ] edgeMode
	- [ ] kernelUnitMode
	- [ ] preserveAlpha
- [ ] [feDiffuseLighting](http://www.w3.org/TR/SVG11/filters.html#feDiffuseLighting)
	- [ ] surfaceScale
	- [ ] diffuseConstant
	- [ ] kernelUnitLength
- [ ] [feDisplacementMap](http://www.w3.org/TR/SVG11/filters.html#feDisplacementMap)
	- [ ] scale
	- [ ] xChannelSelector
	- [ ] yChannelSelector
	- [ ] in2
- [ ] [feFlood](http://www.w3.org/TR/SVG11/filters.html#feFlood)
	- [ ] flood-color
	- [ ] flood-opacity
- [x] [feGaussianBlur](http://www.w3.org/TR/SVG11/filters.html#feGaussianBlur)
	- [x] stdDeviation
- [ ] [feImage](http://www.w3.org/TR/SVG11/filters.html#feImage)
	- [ ] xlink:href
	- [ ] preserveAspectRatio
- [ ] [feMerge](http://www.w3.org/TR/SVG11/filters.html#feMerge)
- [ ] feMergeNode
- [ ] [feMorphology](http://www.w3.org/TR/SVG11/filters.html#feMorphologyElement)
	- [ ] operator
	- [ ] radius
- [x] [feOffset](http://www.w3.org/TR/SVG11/filters.html#feOffset)
	- [x] dx
	- [x] dy
- [x] [feSpecularLighting](http://www.w3.org/TR/SVG11/filters.html#feSpecularLighting)
	- [x] surfaceScale
	- [x] specularConstant
	- [x] specularexponent
	- [x] kernelUnitLength
- [ ] [feTile](http://www.w3.org/TR/SVG11/filters.html#feTile)
- [ ] [feTurbulence](http://www.w3.org/TR/SVG11/filters.html#feTurbulence)
	- [ ] baseFrequency
	- [ ] numOctaves
	- [ ] seed
	- [ ] stitchTiles
	- [ ] type


### [Fonts](http://www.w3.org/TR/SVG11/fonts.html)

- [ ] [font](http://www.w3.org/TR/SVG11/fonts.html#FontElement)
	- [ ] horiz-origin-x
	- [ ] horiz-origin-y
	- [ ] horiz-adv-x
	- [ ] vert-progin-y
	- [ ] vert-adv-y
- [ ] [glyph](http://www.w3.org/TR/SVG11/fonts.html#GlyphElement)
	- [ ] unicode
	- [ ] flyph-name
	- [ ] d
	- [ ] orientation
	- [ ] arabic-form
	- [ ] lang
	- [ ] horiz-adv-x
	- [ ] vert-origin-x
	- [ ] vert-origin-y
	- [ ] vert-adv-x
- [ ] [missing-glyph](http://www.w3.org/TR/SVG11/fonts.html#MissingGlyphElement)
- [ ] hkern and vkern
	- [ ] u1
	- [ ] g1
	- [ ] u2
	- [ ] g2
	- [ ] k
- [ ] [font-face](http://www.w3.org/TR/SVG11/fonts.html#FontFaceElement)
	- [ ] font-family
	- [ ] font-style
	- [ ] font-variant
	- [ ] font-weight
	- [ ] font-stretch
	- [ ] font-size
	- [ ] unicode-range
	- [ ] units-per-em
	- [ ] panose-1
	- [ ] stemv
	- [ ] stemh
	- [ ] slope
	- [ ] cap-height
	- [ ] x-height
	- [ ] accent-height
	- [ ] ascent
	- [ ] descent
	- [ ] widths
	- [ ] bbox
	- [ ] ideographic
	- [ ] alphabetic
	- [ ] mathematical
	- [ ] hanging
	- [ ] v-ideographic
	- [ ] v-alphabetic
	- [ ] v-mathematical
	- [ ] v-hanging
	- [ ] underline-position
	- [ ] underline-thickness
	- [ ] strikethrough-position
	- [ ] strikethrough-thickness
	- [ ] overline-position
	- [ ] overline-thickness
- [ ] [font-face-src](http://www.w3.org/TR/SVG11/fonts.html#FontFaceSrcElement)
- [ ] [font-face-uri](http://www.w3.org/TR/SVG11/fonts.html#FontFaceURIElement)
- [ ] [font-face-format](http://www.w3.org/TR/SVG11/fonts.html#FontFaceFormatElement)
- [ ] [font-face-name](http://www.w3.org/TR/SVG11/fonts.html#FontFaceNameElement)


## License

Copyright 2015 Ben Scott (and contributors)

Licensed under the [MIT License][mit-license]




[mit-license]: https://raw.githubusercontent.com/bendetat/vorticist/master/LICENSE
[svg-1.1-spec]: http://www.w3.org/TR/SVG11/expanded-toc.html
[svg-1.1-spec-styling]: http://www.w3.org/TR/SVG11/styling.html
[svg-1.1-spec-interactivity]: http://www.w3.org/TR/SVG11/interact.html
[svg-1.1-spec-scripting]: http://www.w3.org/TR/SVG11/script.html
[svg-1.1-spec-metadata]: http://www.w3.org/TR/SVG11/metadata.html
[svg-1.1-spec-extensibility]: http://www.w3.org/TR/SVG11/extend.html
[svg-1.1-spec-animation]: http://www.w3.org/TR/SVG11/animate.html
