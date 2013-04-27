using Paneless.Core;
using System.Linq;
using Paneless.Core.Layouts;

namespace Paneless.Layouts
{
    public class HorizontalLayout : Layout
    {

        public override void Tile()
        {
            int numWindows = Windows.Count();
            int windowWidth = Domain.Width/numWindows;
            int windowCount = 0;
            foreach (IWindow window in Windows)
            {
                int windowLeft = Domain.Left + windowCount*windowWidth;
                Rectangle windowDomain = new Rectangle()
                    {
                        Left = windowLeft,
                        Right = windowLeft + windowWidth,
                        Top = Domain.Top,
                        Bottom = Domain.Bottom
                    };
                RenderInDomain(window, windowDomain);
                windowCount++;
            }
        }
    }
}
