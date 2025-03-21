using Microsoft.AspNetCore.Mvc.ModelBinding;
using NonFactors.Mvc.Lookup;

namespace GazaHealthCenter_2.Components.Mvc;

public class TrimmingModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType != typeof(String) || context.Metadata.ContainerType == typeof(LookupFilter))
            return null;

        return new TrimmingModelBinder();
    }
}
