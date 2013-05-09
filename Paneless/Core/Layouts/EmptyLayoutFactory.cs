namespace Paneless.Core.Layouts
{
    public class EmptyLayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string layout)
        {
            return new EmptyLayout();
        }
    }
}
