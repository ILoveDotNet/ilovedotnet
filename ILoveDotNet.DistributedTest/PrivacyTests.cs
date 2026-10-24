using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class PrivacyTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task PrivacyPage_HasMetadataStructuredDataAndResponsivePolicyNavigationAsync()
  {
    const string path = "privacy/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Privacy Policy");
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, path);

    var sections = new[]
    {
      "definitions", "information-collection-and-use", "use-of-data", "transfer-of-data",
      "disclosure-of-data", "security-of-data", "service-providers", "analytics", "advertising",
      "links-to-other-sites", "childrens-privacy", "changes-to-this-privacy-policy", "contact-us",
    };
    var desktopNavigation = Page.GetByRole(AriaRole.Navigation, new() { Name = "Privacy policy sections", Exact = true }).Last;
    await Expect(desktopNavigation).ToBeVisibleAsync();
    foreach (var section in sections)
    {
      var link = desktopNavigation.Locator($"a[href='/privacy/#{section}']");
      await Expect(link).ToHaveCountAsync(1);
      await Expect(Page.Locator($"#{section}")).ToHaveCountAsync(1);
    }
    Assert.True(await Page.Locator("article").EvaluateAsync<bool>(
        "element => { const bounds = element.getBoundingClientRect(); const paragraph = element.querySelector('p'); return bounds.width <= 950 && paragraph.getBoundingClientRect().width <= 75 * parseFloat(getComputedStyle(paragraph).fontSize); }"));
    await Expect(Page.Locator("article button")).ToHaveCountAsync(0);

    await Page.SetViewportSizeAsync(375, 850);
    await Expect(Page.Locator("article details > summary")).ToBeVisibleAsync();
    await Expect(desktopNavigation).ToBeHiddenAsync();
    await Page.Locator("article details > summary").ClickAsync();
    var mobileLink = Page.Locator("article details a[href='/privacy/#advertising']");
    await mobileLink.ClickAsync();
    await Expect(Page.Locator("#advertising")).ToBeInViewportAsync();
    Assert.Contains("#advertising", Page.Url, StringComparison.Ordinal);
    Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
  }
}
