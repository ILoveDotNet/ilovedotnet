using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class AboutTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task AboutPage_HasMetadataStructuredDataAndResponsiveSupportCalloutAsync()
  {
    const string path = "about/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("About I ❤️ .NET");
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, path);

    var support = Page.GetByRole(AriaRole.Complementary, new() { Name = "Help more developers discover I ❤️ .NET" });
    await Expect(support).ToHaveCountAsync(1);
    await Expect(support).ToBeVisibleAsync();
    await Expect(Page.Locator("h2").Last).ToHaveAttributeAsync("id", "community-support-title");
    await Expect(Page.Locator("img[src*='star-request']")).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Sponsors", Exact = true })).ToBeVisibleAsync();
    await Expect(support.GetByRole(AriaRole.Link, new() { Name = "Star on GitHub" })).ToHaveAttributeAsync("href", "https://github.com/ILoveDotNet/ilovedotnet");
    await Expect(support.GetByRole(AriaRole.Link, new() { Name = "Share via WhatsApp" })).ToHaveAttributeAsync("target", "_blank");
    foreach (var width in new[] { 320, 390, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      Assert.True(await support.EvaluateAsync<bool>(
          "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
          $"The About page support callout must fit at {width}px.");
    }
  }
}
