using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UITests.Components.BackToTop;

public class BackToTopTests
{
  [Fact]
  public void TestScrollToTop()
  {
    // Arrange
    using var ctx = new BunitContext();
    var navigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    var cut = ctx.Render<CommonComponents.Shared.BackToTop>();

    // Act
    cut.Find("button").Click();

    // Assert
    Assert.Equal("http://localhost/#app", navigationManager.Uri);
  }
}
