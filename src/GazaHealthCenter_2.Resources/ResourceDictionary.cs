namespace GazaHealthCenter_2.Resources;

internal class ResourceDictionary : ConcurrentDictionary<String, String?>
{
    public ResourceDictionary()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}
