using System.Xml.Linq;
using ILoveDotNet.DistributedTest.Utils;
using static ILoveDotNet.DistributedTest.Utils.SeoTestHelpers;

namespace ILoveDotNet.DistributedTest;

public class SeoTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");
  private static readonly Uri ProductionSeoBaseUrl = new("https://ilovedotnet.org/");
  private readonly Uri ExpectedCanonicalBaseUrl = SeoTestHelpers.GetExpectedCanonicalBaseUrl();

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task NotFoundPage_UsesOneLayoutAndResponsiveRecoveryNavigationAsync()
  {
    foreach (var path in new[] { "notfound", "missing-page-for-layout-test/" })
    {
      await Page.GotoAsync(new Uri(WebBaseUrl, path).ToString());
      await Expect(Page.Locator("h1")).ToHaveTextAsync("Page not found");
      await Expect(Page.Locator("main#main")).ToHaveCountAsync(1);
      await Expect(Page.GetByRole(AriaRole.Banner)).ToHaveCountAsync(1);
      await Expect(Page).ToHaveTitleAsync("Page not found - I ❤️ DotNet");
      await Expect(Page.Locator("meta[name='robots']")).ToHaveAttributeAsync("content", "noindex");
      var content = Page.Locator("section[aria-labelledby='not-found-title']");
      var navigation = content.GetByRole(AriaRole.Navigation, new() { Name = "Not found navigation" });
      await Expect(navigation.GetByRole(AriaRole.Link, new() { Name = "Browse learning paths" })).ToHaveAttributeAsync("href", "/learningpath/");
      await Expect(navigation.GetByRole(AriaRole.Link, new() { Name = "Go home" })).ToHaveAttributeAsync("href", "/");
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        foreach (var size in new[] { (320, 568), (375, 667), (768, 540), (1440, 900) })
        {
          await Page.SetViewportSizeAsync(size.Item1, size.Item2);
          await Expect(content).ToHaveCSSAsync("position", "static");
          Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
          await navigation.GetByRole(AriaRole.Link, new() { Name = "Browse learning paths" }).ScrollIntoViewIfNeededAsync();
          await Expect(navigation.GetByRole(AriaRole.Link, new() { Name = "Browse learning paths" })).ToBeInViewportAsync();
        }
      }
      await Page.SetViewportSizeAsync(path == "notfound" ? 1440 : 375, 900);
      await navigation.GetByRole(AriaRole.Link, new() { Name = "Browse learning paths" }).ClickAsync();
      await Expect(Page.Locator("h1")).ToHaveTextAsync(".NET Tutorial Learning Paths");
      Assert.EndsWith("/learningpath/", Page.Url, StringComparison.Ordinal);
      await Expect(Page.Locator("#not-found-title")).ToHaveCountAsync(0);
      await Expect(Page.Locator("meta[name='robots'][content='noindex']")).ToHaveCountAsync(0);
      await Expect(Page.Locator("main#main")).ToHaveCountAsync(1);
    }
  }

  [Fact]
  public async Task PublicWebsitePages_HaveValidWebsiteStructuredDataAsync()
  {
    foreach (var path in new[] { "chat/" })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(1);
      var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
      try
      {
        Assert.True(documents.Any(document =>
            document.RootElement.TryGetProperty("@type", out var schemaType)
            && schemaType.GetString() == "WebSite"), $"Expected WebSite schema on {path}.");
        var website = GetSchema(documents, "WebSite");
        var websiteUrl = website.RootElement.GetProperty("url").GetString();
        Assert.Equal(SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, path), websiteUrl);

        var mainEntity = website.RootElement.GetProperty("mainEntityOfPage");
        Assert.Equal(SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, path), mainEntity.GetProperty("@id").GetString());
        Assert.True(website.RootElement.TryGetProperty("publisher", out var publisher));
        Assert.Equal("Organization", publisher.GetProperty("@type").GetString());
      }
      finally
      {
        foreach (var document in documents)
        {
          document.Dispose();
        }
      }
    }
  }

  [Fact]
  public async Task LearningChatPage_HasMetadataAndStaticSitemapEntryWithoutNavigationAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/?source=test");
    var canonical = SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, "chat/");
    await Expect(Page).ToHaveTitleAsync("Learn with AI - I ❤️ DotNet");
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", canonical);
    await Expect(Page.Locator("meta[name='description']")).ToHaveAttributeAsync("content",
        "Preview the I Love .NET learning chat interface with suggested questions about .NET, Blazor, and C#. AI responses and article retrieval are not connected yet.");
    await AssertSocialMetadataAsync(Page, canonical, "website", "summary");
    await Expect(Page.Locator("link[rel='sitemap']")).ToHaveAttributeAsync("href",
        SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, "sitemap.xml"));
    await Expect(Page.Locator("link[rel='alternate'][hreflang='en']")).ToHaveAttributeAsync("href", canonical);
    await Expect(Page.Locator("link[rel='alternate'][hreflang='x-default']")).ToHaveAttributeAsync("href", canonical);
    await Expect(Page.Locator("nav a[href='/chat/'], nav a[href='/chat']")).ToHaveCountAsync(0);

    var response = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "sitemap-static.xml"));
    Assert.Equal(200, response.Status);
    var document = XDocument.Parse(await response.TextAsync());
    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    Assert.Single(document.Descendants(ns + "loc"), element =>
        element.Value == "https://ilovedotnet.org/chat/");
  }

  [Fact]
  public async Task RobotsTxt_DeclaresSitemapAndExcludesUtilitiesAsync()
  {
    var response = await Page.GotoAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "robots.txt"));
    Assert.NotNull(response);
    Assert.Equal(200, response.Status);

    var robots = await response.TextAsync();
    Assert.Contains("Sitemap: https://ilovedotnet.org/sitemap.xml", robots, StringComparison.OrdinalIgnoreCase);
    Assert.Contains("Disallow: /atom.xml", robots, StringComparison.OrdinalIgnoreCase);
    Assert.Contains("Disallow: /authentication/", robots, StringComparison.OrdinalIgnoreCase);
    Assert.Contains("Disallow: /notifications", robots, StringComparison.OrdinalIgnoreCase);
  }

  [Fact]
  public async Task SitemapIndex_AndChildSitemapsAreValidAsync()
  {
    var indexResponse = await Page.GotoAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "sitemap.xml"));
    Assert.NotNull(indexResponse);
    Assert.Equal(200, indexResponse.Status);

    var indexDocument = XDocument.Parse(await indexResponse.TextAsync());
    Assert.Equal("sitemapindex", indexDocument.Root?.Name.LocalName);

    XNamespace sitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
    var childLocations = indexDocument
        .Descendants(sitemapNamespace + "loc")
        .Select(element => element.Value)
        .ToList();
    Assert.NotEmpty(childLocations);

    foreach (var childLocation in childLocations)
    {
      var childUri = new Uri(childLocation);
      Assert.Equal(ProductionSeoBaseUrl.Host, childUri.Host);
      Assert.EndsWith(".xml", childUri.AbsolutePath, StringComparison.Ordinal);

      var childResponse = await Page.GotoAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, childUri.AbsolutePath));
      Assert.NotNull(childResponse);
      Assert.Equal(200, childResponse.Status);

      var childDocument = XDocument.Parse(await childResponse.TextAsync());
      Assert.Equal("urlset", childDocument.Root?.Name.LocalName);

      foreach (var location in childDocument.Descendants(sitemapNamespace + "loc").Select(element => element.Value))
      {
        var uri = new Uri(location);
        Assert.Equal(ProductionSeoBaseUrl.Host, uri.Host);
        Assert.Empty(uri.Query);
        Assert.EndsWith("/", uri.AbsolutePath, StringComparison.Ordinal);
      }
    }
  }

  [Fact]
  public async Task AtomFeed_ContainsCanonicalEntriesAsync()
  {
    var response = await Page.GotoAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "atom.xml"));
    Assert.NotNull(response);
    Assert.Equal(200, response.Status);

    var document = XDocument.Parse(await response.TextAsync());
    XNamespace atomNamespace = "http://www.w3.org/2005/Atom";
    Assert.Equal("feed", document.Root?.Name.LocalName);

    var entries = document.Descendants(atomNamespace + "entry").ToList();
    Assert.NotEmpty(entries);

    var links = entries
        .Select(entry => entry.Elements(atomNamespace + "link")
            .FirstOrDefault(link => (string?)link.Attribute("rel") is null || (string?)link.Attribute("rel") == "alternate")
            ?.Attribute("href")?.Value)
        .ToList();
    Assert.DoesNotContain(links, link => string.IsNullOrWhiteSpace(link));
    Assert.Equal(links.Count, links.Distinct(StringComparer.OrdinalIgnoreCase).Count());

    foreach (var link in links)
    {
      var uri = new Uri(link!);
      Assert.Equal(ProductionSeoBaseUrl.Host, uri.Host);
      Assert.Empty(uri.Query);
      Assert.EndsWith("/", uri.AbsolutePath, StringComparison.Ordinal);
      Assert.DoesNotContain("/MCP/", uri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
      Assert.DoesNotContain("/Database/", uri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
      Assert.DoesNotContain("/Caching/", uri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
    }

    foreach (var entry in entries)
    {
      Assert.False(string.IsNullOrWhiteSpace(entry.Element(atomNamespace + "title")?.Value));
      Assert.False(string.IsNullOrWhiteSpace(entry.Element(atomNamespace + "summary")?.Value));
      Assert.False(string.IsNullOrWhiteSpace(entry.Element(atomNamespace + "updated")?.Value));
    }
  }
}
