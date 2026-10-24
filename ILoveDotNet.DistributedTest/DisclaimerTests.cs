using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class DisclaimerTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task DisclaimerPage_HasMetadataStructuredDataAndResponsiveRelatedNavigationAsync()
  {
    const string path = "disclaimer/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Disclaimer");
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, path);
    await Expect(Page.Locator("meta[name='description']")).ToHaveAttributeAsync("content",
        "Read the I Love DotNet disclaimer covering informational content, external website links, responsibility, and updates.");
    await Expect(Page.Locator("meta[property='og:image']")).ToHaveAttributeAsync("content",
        $"{SeoTestHelpers.GetExpectedCanonicalBaseUrl()}image/brand/mini-logo.png");

    var document = Page.Locator("section[aria-labelledby='disclaimer-title']");
    var terms = document.GetByRole(AriaRole.Article, new() { Name = "Disclaimer terms" });
    await Expect(terms.Locator("h2")).ToHaveTextAsync(
        ["Information and responsibility", "External websites", "Consent", "Updates"]);
    await Expect(terms).ToContainTextAsync("By using our website, you hereby consent to our disclaimer and agree to its terms.");
    await Expect(document.GetByRole(AriaRole.Link, new() { Name = "Contact Us", Exact = true }))
        .ToHaveAttributeAsync("href", "https://www.linkedin.com/in/fingers10");
    var related = document.GetByRole(AriaRole.Navigation, new() { Name = "Related pages" });
    await Expect(related.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy", Exact = true }))
        .ToHaveAttributeAsync("href", "/privacy/");
    await Expect(related.GetByRole(AriaRole.Link, new() { Name = "About I Love DotNet", Exact = true }))
        .ToHaveAttributeAsync("href", "/about/");
    Assert.True(await document.EvaluateAsync<bool>(
        "element => element.querySelector('header').getBoundingClientRect().bottom <= element.querySelector('article').getBoundingClientRect().top"));
    foreach (var dark in new[] { false, true })
    {
      await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
      foreach (var width in new[] { 320, 375, 768, 1280, 1440 })
      {
        await Page.SetViewportSizeAsync(width, 900);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Disclaimer must fit at {width}px in {(dark ? "dark" : "light")} mode.");
        Assert.True(await terms.Locator("p").First.EvaluateAsync<bool>(
            "element => element.getBoundingClientRect().width <= parseFloat(getComputedStyle(element).maxWidth) + 1"));
      }
    }
    await related.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy", Exact = true }).ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Privacy Policy");
  }
}
