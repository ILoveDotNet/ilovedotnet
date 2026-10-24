using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class CareerTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task CareerPage_HasMetadataAndResponsiveGuideWithWorkingNavigationAsync()
  {
    const string path = "career/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Developer career growth");
    await Expect(Page.Locator("main h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);

    var headingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>(
        "headings => headings.map(heading => heading.id)");
    Assert.Equal(new[] { "career-action-title", "career-foundations-title", "career-growth-title", "career-interview-title" }, headingIds);
    var cards = Page.Locator("section[aria-labelledby='career-growth-title'] article");
    await Expect(cards).ToHaveCountAsync(7);
    await Expect(Page.GetByRole(AriaRole.Complementary, new() { Name = "Conversation starter" })).ToBeVisibleAsync();
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var darkMode in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", darkMode);
        Assert.True(await Page.EvaluateAsync<bool>(
            "() => { const main = document.querySelector('main'); return main.scrollWidth <= main.clientWidth && main.getBoundingClientRect().right <= innerWidth; }"),
            $"Career should not overflow at {width}px in {(darkMode ? "dark" : "light")} mode.");
        var first = await cards.Nth(0).BoundingBoxAsync();
        var second = await cards.Nth(1).BoundingBoxAsync();
        Assert.NotNull(first);
        Assert.NotNull(second);
        if (width < 768)
        {
          Assert.True(second.Y > first.Y, "Substantial cards should stack on mobile.");
        }
        else
        {
          Assert.Equal(first.Y, second.Y);
          Assert.True(second.X > first.X, "Substantial cards should use two columns on larger screens.");
        }
      }
    }

    foreach (var link in await Page.Locator("nav[aria-label='Career guide topics'] a").AllAsync())
    {
      await link.ClickAsync();
      var target = new Uri(Page.Url).Fragment;
      await Expect(Page.Locator(target)).ToBeInViewportAsync();
    }
    await Page.GetByRole(AriaRole.Link, new() { Name = "Explore the .NET interview preparation guide" }).ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("C# and .NET Interview Questions and Answers");
  }
}
