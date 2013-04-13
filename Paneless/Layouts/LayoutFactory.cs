using Paneless.Core;
using Paneless.Layouts;

namespace Layouts
{
    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string layout)
        {
            switch (layout)
            {
                case("CompositeHorizontalLayout"):
                    return new CompositeHorizontalLayout();
                case("FullDomainLayout"):
                    return new FullDomainLayout();
                case("HorizontalLayout"):
                    return new HorizontalLayout();
                case("VerticalLayout"):
                    return new VerticalLayout();
                default:
                    return new CompositeHorizontalLayout();
            }
        }
    }
}
