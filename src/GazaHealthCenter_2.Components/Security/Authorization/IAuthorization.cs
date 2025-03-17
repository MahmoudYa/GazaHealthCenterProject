namespace GazaHealthCenter_2.Components.Security;

public interface IAuthorization
{
    Boolean IsGrantedFor(Int64 accountId, String permission);

    void Refresh(IServiceProvider services);
}
