using Microsoft.AspNetCore.Mvc.Rendering;

namespace GazaHealthCenter_2.Components.Mvc;

public interface ISiteMap
{
    SiteMapNode[] For(ViewContext context);
    SiteMapNode[] BreadcrumbFor(ViewContext context);
}
