namespace Paneless.Core
{
    public interface ILayoutFactory
    {
        ILayout CreateLayout(string layout); // TODO In future this won't be a string
    }
}
