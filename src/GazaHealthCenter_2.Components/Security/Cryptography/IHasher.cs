namespace GazaHealthCenter_2.Components.Security;

public interface IHasher
{
    String Hash(String value);
    String HashPassword(String value);

    Boolean Verify(String? value, String? hash);
    Boolean VerifyPassword(String? value, String? passhash);
}
