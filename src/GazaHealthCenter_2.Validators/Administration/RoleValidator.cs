using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects;
using GazaHealthCenter_2.Resources;

namespace GazaHealthCenter_2.Validators;

public class RoleValidator : AValidator
{
    public RoleValidator(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }

    public Boolean CanCreate(RoleView view)
    {
        Boolean isValid = IsUniqueTitle(0, view.Title);
        isValid &= ModelState.IsValid;

        return isValid;
    }
    public Boolean CanEdit(RoleView view)
    {
        Boolean isValid = IsUniqueTitle(view.Id, view.Title);
        isValid &= ModelState.IsValid;

        return isValid;
    }

    private Boolean IsUniqueTitle(Int64 id, String title)
    {
        Boolean isUnique = !UnitOfWork
            .Select<Role>()
            .Any(role =>
                role.Id != id &&
                role.Title == title);

        if (!isUnique)
            ModelState.AddModelError(nameof(RoleView.Title), Validation.For<RoleView>("UniqueTitle"));

        return isUnique;
    }
}
