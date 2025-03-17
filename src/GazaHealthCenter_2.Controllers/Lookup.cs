using Microsoft.AspNetCore.Mvc;
using GazaHealthCenter_2.Components.Lookups;
using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects;
using NonFactors.Mvc.Lookup;

namespace GazaHealthCenter_2.Controllers;

[AllowUnauthorized]
public class Lookup : AController
{
    private IUnitOfWork UnitOfWork { get; }

    public Lookup(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    [HttpGet]
    public JsonResult Role(LookupFilter filter)
    {
        return Json(new MvcLookup<Role, RoleView>(UnitOfWork) { Filter = filter }.GetData());
    }

    protected override void Dispose(Boolean disposing)
    {
        UnitOfWork.Dispose();

        base.Dispose(disposing);
    }
}
