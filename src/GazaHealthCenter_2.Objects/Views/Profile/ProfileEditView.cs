using GazaHealthCenter_2.Components.Mvc;

namespace GazaHealthCenter_2.Objects;

public class ProfileEditView : AView<Account>
{
    [StringLength(32)]
    public String Username { get; set; }

    [NotTrimmed]
    [StringLength(32)]
    public String Password { get; set; }

    [NotTrimmed]
    [StringLength(32)]
    public String? NewPassword { get; set; }

    [EmailAddress]
    [StringLength(256)]
    public String Email { get; set; }
}
