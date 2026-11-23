using System.Xml.Linq;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class SeoTests(AppFixture fixture) : DistributedPageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");
  private static readonly Uri ProductionSeoBaseUrl = new("https://ilovedotnet.org/");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

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
      if (childUri.AbsolutePath.EndsWith("/sitemap-static.xml", StringComparison.Ordinal))
      {
        Assert.Contains("https://ilovedotnet.org/sponsor/",
            childDocument.Descendants(sitemapNamespace + "loc").Select(element => element.Value));
      }

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
  public async Task SponsorshipPage_IsDiscoverableAndResponsiveAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var sponsorLink = Page.GetByRole(AriaRole.Link, new() { Name = "Explore sponsorship opportunities" });
    await Expect(sponsorLink).ToHaveAttributeAsync("href", "/sponsor/");
    await sponsorLink.ClickAsync();
    await Expect(Page).ToHaveURLAsync(new Uri(WebBaseUrl, "sponsor/").AbsoluteUri);
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Help developers learn. Put your brand in the conversation." })).ToBeVisibleAsync();
    var canonical = await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, "sponsor/");
    var schemas = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      Assert.Equal(canonical, SeoTestHelpers.GetSchema(schemas, "WebSite").RootElement.GetProperty("url").GetString());
      SeoTestHelpers.AssertBreadcrumbSchema(SeoTestHelpers.GetSchema(schemas, "BreadcrumbList"));
    }
    finally
    {
      foreach (var schema in schemas)
      {
        schema.Dispose();
      }
    }
    var published = new SharedModels.TableOfContents().Contents;
    await Expect(Page.Locator("main")).ToContainTextAsync($"{published.Count} published tutorials");
    await Expect(Page.Locator("main")).ToContainTextAsync($"{published.Select(content => content.Channel).Distinct(StringComparer.OrdinalIgnoreCase).Count()} topics");
    foreach (var name in new[] { "Discuss sponsorship on LinkedIn", "Message Abdul on LinkedIn" })
    {
      await Expect(Page.GetByRole(AriaRole.Link, new() { Name = name }))
          .ToHaveAttributeAsync("href", "https://www.linkedin.com/in/thebhai");
    }
    await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Discuss sponsorship on LinkedIn" })).ToBeVisibleAsync();
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Sponsorship page must fit at {width}px in {(dark ? "dark" : "light")} mode.");
      }
    }
  }

  [Fact]
  public async Task SponsorshipPage_LoadsFromDirectUrlAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "sponsor/");
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Help developers learn. Put your brand in the conversation." })).ToBeVisibleAsync();
    await Expect(Page.Locator("nav[aria-label='Breadcrumb']")).ToBeVisibleAsync();
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
