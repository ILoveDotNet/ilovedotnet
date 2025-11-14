namespace UITests.Components.Others;

[ExcludeFromCodeCoverage]
public class OthersTests
{
  [Fact]
  public void TestMenuToggle()
  {
    // Arrange
    using var ctx = new BunitContext();
    var cut = ctx.Render<CommonComponents.Shared.Others>();

    // Act
    cut.Find("button").Click();

    // Assert
    var menu = cut.Find("#others > div");
    Assert.Equal("dropdown is-right is-active", menu.GetAttribute("class"));
  }

  [Fact]
  public async Task TestFocusOutHandlerAsync()
  {
    // Arrange
    using var ctx = new BunitContext();
    var cut = ctx.Render<CommonComponents.Shared.Others>();

    // Act
    await cut.Find("button").ClickAsync();
    await Task.Delay(500, Xunit.TestContext.Current.CancellationToken);
    await cut.Find("a").ClickAsync();
    await Task.Delay(500, Xunit.TestContext.Current.CancellationToken);

    // Assert
    var menu = cut.Find("#others > div");
    Assert.Equal("dropdown is-right ", menu.GetAttribute("class"));
  }
}
