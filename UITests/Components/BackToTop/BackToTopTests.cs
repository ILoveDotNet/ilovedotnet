using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UITests.Components.Navigation;

public class BackToTopTests
{
  [Fact]
  public void TestScrollToTop()
  {
    // Arrange
    using var ctx = new TestContext();
    var navigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
    var cut = ctx.RenderComponent<CommonComponents.Shared.BackToTop>();

    // Act
    cut.Find("button").Click();

    // Assert
    Assert.Equal("http://localhost/#app", navigationManager.Uri);
  }
}
