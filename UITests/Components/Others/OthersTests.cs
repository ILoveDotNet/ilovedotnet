namespace UITests.Components.Others;

[ExcludeFromCodeCoverage]
public class OthersTests
{
  [Theory]
  [InlineData("/about", "About")]
  [InlineData("/analytics", "Analytics")]
  [InlineData("/disclaimer", "Disclaimer")]
  [InlineData("/privacy", "Privacy")]
  public void SecondaryPageLink_RendersAndClosesMenuWhenClicked(string route, string label)
  {
    using var ctx = new BunitContext();
    var cut = ctx.Render<CommonComponents.Shared.Others>();

    cut.Find("button").Click();
    var link = cut.Find($"#others-menu a[href='{route}']");
    Assert.Equal(label, link.TextContent.Trim());

    link.Click();

    Assert.DoesNotContain("is-active", cut.Find("#others > div").ClassList);
  }

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
