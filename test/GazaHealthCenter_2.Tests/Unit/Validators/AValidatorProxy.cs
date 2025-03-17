using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects;

namespace GazaHealthCenter_2.Validators;

public class AValidatorProxy : AValidator
{
    public AValidatorProxy(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }

    public Boolean BaseIsSpecified<TView>(TView view, Expression<Func<TView, Object?>> property) where TView : AView
    {
        return IsSpecified(view, property);
    }
}
