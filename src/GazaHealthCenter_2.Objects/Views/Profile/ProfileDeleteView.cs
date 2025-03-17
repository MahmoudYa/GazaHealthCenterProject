using GazaHealthCenter_2.Components.Mvc;

namespace GazaHealthCenter_2.Objects;

public class ProfileDeleteView : AView
{
    [NotTrimmed]
    [StringLength(32)]
    public String Password { get; set; }
}
