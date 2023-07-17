namespace UITests.Components.Others;

[ExcludeFromCodeCoverage]
public class OthersTests
{
    [Fact]
    public void TestMenuToggle()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Web.Shared.Others>();

        // Act
        cut.Find("button").Click();

        // Assert
        var menu = cut.Find("#others > div");
        Assert.Equal("dropdown is-right is-active", menu.GetAttribute("class"));
    }

    [Fact]
    public async Task TestFocusOutHandler()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Web.Shared.Others>();

        // Act
        cut.Find("button").Click();
        await Task.Delay(500);
        cut.Find("a").Click();
        await Task.Delay(500);

        // Assert
        var menu = cut.Find("#others > div");
        Assert.Equal("dropdown is-right ", menu.GetAttribute("class"));
    }
}