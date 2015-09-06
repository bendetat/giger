using Giger.Nancy;
using Giger.Shapes;
using Giger.Text;
using Nancy;

namespace Giger.TestSite.Examples.Text
{
    public class MultiLineTextModule : NancyModule
    {
        public MultiLineTextModule()
        {
            Get["examples/text/multi-line-text/howl"] = _ =>
            {
                var svg = new Svg(600, 160);

                svg.WithBackgroundFill("#ddd");

                var howl = @"I saw the best minds of my generation destroyed by madness, starving hysterical naked,
dragging themselves through the negro streets at dawn looking for an angry fix,
Angel-headed hipsters burning for the ancient heavenly connection
to the starry dynamo in the machinery of night
- Howl, Allen Ginsberg";

                svg.MultiLineText(20,20, howl);

                return Response.AsSvg(svg);
            };

            Get["examples/text/multi-line-text/haiku"] = _ =>
            {
                var svg = new Svg(400, 100);

                svg.WithBackgroundFill("#dde");

                var haiku = @"the first cold shower
even the monkey seems to want
a little coat of straw";

                svg.MultiLineText((svg.Width ?? 0)/2, 20, haiku)
                    .WithTextAnchor(TextAnchor.Middle)
                    .WithFontStyle(FontStyle.Italic)
                    .WithFontFamily(FontFamilies.Helvetica);

                return Response.AsSvg(svg);
            };

            Get["examples/text/multi-line-text/ulysses"] = _ =>
            {
                var svg = new Svg(250, 700);

                svg.WithBackgroundFill("#333");

                var text = @"Stately, plump Buck Mulligan came from the stairhead, bearing a bowl of lather on which a mirror and a razor lay crossed. A yellow dressinggown, ungirdled, was sustained gently behind him by the mild morning air.";

                svg.MultiLineText(140, 0, text)
                    .WithAutoLineSplit(10)
                    .WithFontFamily(FontFamilies.Helvetica)
                    .WithFill("gold")
                    .WithTransformRotate(10);

                return Response.AsSvg(svg);
            };

            Get["examples/text/multi-line-text/show-origin"] = _ =>
            {
                var svg = new Svg(400, 200);

                svg.WithBackgroundFill("#ddd");

                var text = @"FALSTAFF: Come, thou shalt go to the wars in a
gown. We will have away thy cold; and I will
take such order that thy friends shall ring for
thee. Is here all?";

                svg.MultiLineText(30, 30, text);
                svg.Circle(30, 30, 2).WithFill("red");

                return Response.AsSvg(svg);
            };


            Get["examples/text/multi-line-text/element-line-height"] = _ =>
            {
                var svg = new Svg(550, 250);

                svg.WithBackgroundFill("#ddd");

                var text = @"No one would have believed, in the last years of the nineteenth century, that human affairs were being watched keenly and closely by intelligences greater than man’s and yet as mortal as his own; that as men busied themselves about their affairs they were scrutinized and studied, perhaps almost as narrowly as a man with a microscope might scrutinize the transient creatures that swarm and multiply in a drop of water.";

                var textElement = svg.MultiLineText(30, 40, text)
                    .WithAutoLineSplit(60);
                svg.Line((textElement.X ?? 0) - 14, (textElement.Y ?? 0), (textElement.X ?? 0) - 14, (textElement.Y ?? 0) + (textElement.Height ?? 0))
                    .WithStroke("brown")
                    .WithStrokeLinecap(StrokeLinecap.Round)
                    .WithStrokeWidth(3);

                return Response.AsSvg(svg);
            };


        }
    }
}