using Paneless.Core;
using System.Linq;
using Paneless.Core.Layouts;

namespace Paneless.Layouts
{
    public class VerticalLayout : Layout
    {
        public override void Tile()
        {
            int numWindows = Windows.Count();
            int windowHeight = Domain.Height / numWindows;
            int windowCount = 0;
            foreach (IWindow window in Windows)
            {
                int windowTop = Domain.Top + windowCount * windowHeight;
                Rectangle windowDomain = new Rectangle()
                {
                    Left = Domain.Left,
                    Right = Domain.Right,
                    Top = windowTop,
                    Bottom = windowTop + windowHeight,
                };
                RenderInDomain(window, windowDomain);
                windowCount++;
            }
        }
    }
}
