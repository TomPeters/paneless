using Paneless.Core;
using Paneless.Core.Layouts;

namespace Paneless.Layouts
{
    public class CompositeHorizontalLayout : Layout
    {

        public override void Tile()
        {
            switch (Windows.Count)
            {
                case(0):
                    break;
                case (1):
                    {
                        RenderInDomain(Windows[0], Domain);
                        break;
                    }
                default:
                    {
                        RenderInDomain(Windows[0], new Rectangle()
                            {
                                Top = Domain.Top,
                                Bottom = Domain.Bottom,
                                Left = Domain.Left,
                                Right = Domain.Left + Domain.Width/2
                            });
                        VerticalLayout verticalLayout = new VerticalLayout();
                        for (int windowIndex = 1; windowIndex < Windows.Count; windowIndex++)
                        {
                            verticalLayout.AddWindowsWithoutTile(Windows[windowIndex]);
                        }
                        verticalLayout.Domain = new Rectangle()
                            {
                                Top = Domain.Top,
                                Bottom = Domain.Bottom,
                                Left = Domain.Left + Domain.Width/2,
                                Right = Domain.Right
                            };
                        verticalLayout.Tile();
                        break;
                    }
            }
        }
    }
}
