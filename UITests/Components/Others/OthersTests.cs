using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace UITests.Components.Others;

[ExcludeFromCodeCoverage]
public class OthersTests
{
  [Theory]
  [InlineData("System", "system")]
  [InlineData("Light", "light")]
  [InlineData("Dark", "dark")]
  public async Task MobileTheme_ChangesSavedChoiceWithoutClosingPanelAsync(string mode, string expected)
  {
    using var ctx = new BunitContext();
    var module = ctx.JSInterop.SetupModule("./js/displaymode.js");
    module.Mode = JSRuntimeMode.Loose;
    module.Setup<string>("getDisplayMode").SetResult("dark");
    var cut = ctx.Render<CascadingValue<bool>>(parameters => parameters
        .Add(value => value.Value, true)
        .AddChildContent<CommonComponents.Shared.Others>());

    await cut.Find("button").ClickAsync();
    Assert.True(cut.Find("input[value='Dark']").HasAttribute("checked"));
    var losingFocus = cut.Find("#others > div").FocusOutAsync();
    var radio = cut.Find($"input[value='{mode}']");
    await radio.TriggerEventAsync("onfocusin", new FocusEventArgs());
    await radio.ChangeAsync(new ChangeEventArgs { Value = true });
    await losingFocus;

    Assert.True(cut.Find($"input[value='{mode}']").HasAttribute("checked"));
    Assert.Equal(expected, module.Invocations["onDisplayModeChanged"].Single().Arguments[0]);
    Assert.Contains("is-active", cut.Find("#others > div").ClassList);
    Assert.Empty(cut.FindAll("button[aria-label='Theme Selector']"));
  }

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

  [Fact]
  public async Task DelayedFocusOut_DoesNotCloseReopenedMenuAsync()
  {
    using var ctx = new BunitContext();
    var cut = ctx.Render<CommonComponents.Shared.Others>();
    await cut.Find("button").ClickAsync();

    var focusOut = cut.Find("#others > div").FocusOutAsync();
    await cut.Find("#others-menu a[href='/about']").ClickAsync();
    await cut.Find("button").ClickAsync();
    await focusOut;

    Assert.Contains("is-active", cut.Find("#others > div").ClassList);
  }
}
