using System.Xml.Linq;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task LearningChatPage_HasMetadataStructuredDataAndStaticSitemapEntryWithoutNavigationAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/?source=test");
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, "chat/");
    var canonicalBaseUrl = SeoTestHelpers.GetExpectedCanonicalBaseUrl();
    var canonical = SeoTestHelpers.ExpectedUrl(canonicalBaseUrl, "chat/");
    await Expect(Page).ToHaveTitleAsync("Learn with AI - I ❤️ DotNet");
    await Expect(Page.Locator("meta[name='description']")).ToHaveAttributeAsync("content",
        "Preview the I Love .NET learning chat interface with suggested questions about .NET, Blazor, and C#. AI responses and article retrieval are not connected yet.");
    await SeoTestHelpers.AssertSocialMetadataAsync(Page, canonical, "website", "summary");
    await Expect(Page.Locator("link[rel='sitemap']")).ToHaveAttributeAsync("href",
        SeoTestHelpers.ExpectedUrl(canonicalBaseUrl, "sitemap.xml"));
    await Expect(Page.Locator("nav a[href='/chat/'], nav a[href='/chat']")).ToHaveCountAsync(0);

    var response = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "sitemap-static.xml"));
    Assert.Equal(200, response.Status);
    var document = XDocument.Parse(await response.TextAsync());
    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    Assert.Single(document.Descendants(ns + "loc"), element =>
        element.Value == "https://ilovedotnet.org/chat/");
  }
}
