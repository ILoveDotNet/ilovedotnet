using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SharedComponents;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class SlugServiceTests
{
  [Fact]
  public void TestSlugFromValidUrl()
  {
    using var ctx = new BunitContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/test-slug");

    var slugService = new SlugService(fakeNavigationManager);
    var result = slugService.GetSlug();

    Assert.Equal("test-slug", result);
  }

  [Fact]
  public void TestSlugFromUrlEndingWithSlash()
  {
    using var ctx = new BunitContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/test-slug/");

    var slugService = new SlugService(fakeNavigationManager);
    var result = slugService.GetSlug();

    Assert.Equal("test-slug", result);
  }

  [Fact]
  public void TestSlugFromUrlWithoutSlug()
  {
    using var ctx = new BunitContext();
    var fakeNavigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    fakeNavigationManager.NavigateTo("http://example.com/");
    var slugService = new SlugService(fakeNavigationManager);

    Assert.Throws<ArgumentException>(() => slugService.GetSlug());
  }

  [Theory]
  [InlineData("https://ilovedotnet.org/blogs/test-slug")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug/")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug?source=newsletter")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug/?source=newsletter")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug#how")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug/#how")]
  [InlineData("https://ilovedotnet.org/blogs/test-slug/?source=newsletter#summary")]
  public void TestSlugIgnoresQueryAndFragment(string url)
  {
    using var ctx = new BunitContext();
    var navigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    navigationManager.NavigateTo(url);
    var slugService = new SlugService(navigationManager);

    Assert.Equal("test-slug", slugService.GetSlug());
  }

  [Theory]
  [InlineData("https://ilovedotnet.org")]
  [InlineData("https://ilovedotnet.org/")]
  [InlineData("https://ilovedotnet.org/?source=newsletter")]
  [InlineData("https://ilovedotnet.org/#how")]
  [InlineData("https://ilovedotnet.org/?source=newsletter#summary")]
  public void TestMissingSlugPreservesExceptionContract(string url)
  {
    using var ctx = new BunitContext();
    var navigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    navigationManager.NavigateTo(url);
    var slugService = new SlugService(navigationManager);

    var exception = Assert.Throws<ArgumentException>(() => slugService.GetSlug());
    Assert.Equal("slug", exception.ParamName);
  }

  [Fact]
  public void TestSlugUpdatesWhenNavigatingBetweenArticlesAndAnchors()
  {
    using var ctx = new BunitContext();
    var navigationManager = ctx.Services.GetRequiredService<BunitNavigationManager>();
    var slugService = new SlugService(navigationManager);

    navigationManager.NavigateTo("https://ilovedotnet.org/blogs/first-article/");
    Assert.Equal("first-article", slugService.GetSlug());
    navigationManager.NavigateTo("https://ilovedotnet.org/blogs/first-article/?source=test#how");
    Assert.Equal("first-article", slugService.GetSlug());
    navigationManager.NavigateTo("https://ilovedotnet.org/blogs/second-article/#summary");
    Assert.Equal("second-article", slugService.GetSlug());
  }
}
