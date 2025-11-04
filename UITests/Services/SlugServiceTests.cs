using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SharedComponents;
using TestContext = Bunit.TestContext;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class SlugServiceTests
{
  [Fact]
  public void TestSlugFromValidUrl()
  {
    using var ctx = new TestContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/test-slug");

    var slugService = new SlugService(fakeNavigationManager);
    var result = slugService.GetSlug();

    Assert.Equal("test-slug", result);
  }

  [Fact]
  public void TestSlugFromUrlEndingWithSlash()
  {
    using var ctx = new TestContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/test-slug/");

    var slugService = new SlugService(fakeNavigationManager);
    var result = slugService.GetSlug();

    Assert.Equal("test-slug", result);
  }

  [Fact]
  public void TestSlugFromUrlWithoutSlug()
  {
    using var ctx = new TestContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/");
    var slugService = new SlugService(fakeNavigationManager);

    Assert.Throws<ArgumentException>(() => slugService.GetSlug());
  }
}
