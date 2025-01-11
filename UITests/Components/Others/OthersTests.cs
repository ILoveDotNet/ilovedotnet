namespace UITests.Components.Others;

[ExcludeFromCodeCoverage]
public class OthersTests
{
  [Fact]
  public void TestMenuToggle()
  {
    // Arrange
    using var ctx = new TestContext();
    var cut = ctx.RenderComponent<CommonComponents.Shared.Others>();

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
    using var ctx = new TestContext();
    var cut = ctx.RenderComponent<CommonComponents.Shared.Others>();

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
