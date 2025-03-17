using GazaHealthCenter_2.Components.Tree;

namespace GazaHealthCenter_2.Objects;

public class RoleViewTests
{
    [Fact]
    public void RoleView_CreatesEmpty()
    {
        MvcTree actual = new RoleView().Permissions;

        Assert.Empty(actual.SelectedIds);
        Assert.Empty(actual.Nodes);
    }
}
