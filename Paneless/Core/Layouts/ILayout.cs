namespace Paneless.Core.Layouts
{
    public interface ILayout
    {
        void Tile();
        Rectangle Domain { set; }
        void AddWindow(IWindow window);
        void AddWindowsWithoutTile(IWindow window);
    }
}
