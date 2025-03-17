using GazaHealthCenter_2.Resources;

namespace GazaHealthCenter_2.Components.Mvc;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DigitsAttribute : ValidationAttribute
{
    public DigitsAttribute()
        : base(() => Validation.For("Digits"))
    {
    }

    public override Boolean IsValid(Object? value)
    {
        return value?.ToString() is not String input || Regex.IsMatch(input, "^[0-9]+$");
    }
}
