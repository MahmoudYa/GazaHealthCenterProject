using GazaHealthCenter_2.Resources;

namespace GazaHealthCenter_2.Components.Mvc;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class IntegerAttribute : ValidationAttribute
{
    public IntegerAttribute()
        : base(() => Validation.For("Integer"))
    {
    }

    public override Boolean IsValid(Object? value)
    {
        return value?.ToString() is not String input || Regex.IsMatch(input, "^[+-]?[0-9]+$");
    }
}
