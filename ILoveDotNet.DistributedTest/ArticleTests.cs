using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class ArticleTests(AppFixture fixture) : DistributedPageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 800 },
    IgnoreHTTPSErrors = true,
  };

  [Theory]
  [InlineData(320)]
  [InlineData(390)]
  [InlineData(1280)]
  public async Task Article_SubsectionLinksPreserveQueriesAndFitTheReaderAsync(int width)
  {
    await Page.SetViewportSizeAsync(width, 800);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl,
        "blogs/understanding-caching-fundamentals-in-dotnet/?source=reader");
    var target = Page.GetByRole(AriaRole.Heading, new() { Name = "When to Cache", Exact = true });
    await Expect(target).ToHaveAttributeAsync("id", "when-to-cache");
    await Expect(Page.Locator("article[aria-labelledby='article-title'] > header figure")).ToHaveCountAsync(0);
    var jump = Page.GetByRole(AriaRole.Link, new() { Name = "Jump to implementation", Exact = true });
    await Expect(jump).ToHaveAttributeAsync("href",
        "/blogs/understanding-caching-fundamentals-in-dotnet/?source=reader#how");
    if (width < 1280)
    {
      await Page.Locator("article > details > summary").ClickAsync();
    }

    var contents = Page.Locator("nav[aria-label='On this page']:visible");
    var link = contents.GetByRole(AriaRole.Link, new() { Name = "When to Cache", Exact = true });
    await link.ClickAsync();
    Assert.EndsWith("?source=reader#when-to-cache", Page.Url);
    Assert.True(await target.EvaluateAsync<bool>(
        "element => { const bounds = element.getBoundingClientRect(); return bounds.top >= document.querySelector('#header').getBoundingClientRect().bottom && bounds.top < innerHeight; }"));
    Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
    await Page.ReloadAsync();
    await Expect(target).ToHaveAttributeAsync("id", "when-to-cache");
    await Expect(target).ToBeInViewportAsync();
  }
}
