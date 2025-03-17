using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using GazaHealthCenter_2.Resources;

namespace GazaHealthCenter_2.Components.Mvc;

public class IntegerValidator : IClientModelValidator
{
    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val-integer"))
            context.Attributes["data-val-integer"] = Validation.For("Integer", context.ModelMetadata.GetDisplayName());
    }
}
