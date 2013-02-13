using Paneless.Core;

namespace Paneless.Layouts
{
    public class FullDomainLayout : LayoutBase
    {
        public FullDomainLayout()
        {
            BorderWidth = 0;
        }

        public FullDomainLayout(int borderWidth)
        {
            BorderWidth = borderWidth;
        }

        public override void Tile()
        {
            //Tile all as full screen
            foreach (IWindow window in Windows)
            {
                TileAsFullScreen(window);
            }
        }

        public int BorderWidth { private get; set; }

        private void TileAsFullScreen(IWindow window)
        {
            Rectangle windowDomain = new Rectangle()
                {
                    Top = Domain.Top + BorderWidth,
                    Bottom = Domain.Bottom - BorderWidth,
                    Left = Domain.Left + BorderWidth,
                    Right = Domain.Right - BorderWidth
                };
            window.SetLocation(windowDomain);
        }
    }
}
