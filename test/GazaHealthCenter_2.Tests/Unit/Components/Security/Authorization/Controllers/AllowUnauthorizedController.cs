using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GazaHealthCenter_2.Components.Security;

[AllowUnauthorized]
[ExcludeFromCodeCoverage]
public class AllowUnauthorizedController : AuthorizeController
{
    [HttpGet]
    public ViewResult AuthorizedAction()
    {
        return View();
    }
}
