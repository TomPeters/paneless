using Paneless.Core;

namespace Layouts
{
    public class FullScreenLayout : LayoutBase
    {
        public override void Tile()
        {
            //Tile all as full screen
            foreach (IWindow window in Windows)
            {
                TileAsFullScreen(window);
            }
        }

        private void TileAsFullScreen(IWindow window)
        {
            window.SetLocation(Domain);
        }
    }
}
