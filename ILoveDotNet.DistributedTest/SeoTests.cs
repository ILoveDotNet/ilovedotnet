using System.Text.Json;
using System.Xml.Linq;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class SeoTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");
#if DEBUG
  private const string DefaultSeoBaseUrl = "https://localhost:7176/";
#else
  private const string DefaultSeoBaseUrl = "https://ilovedotnet.org/";
#endif
  private static readonly Uri ProductionSeoBaseUrl = new("https://ilovedotnet.org/");
  private readonly Uri ExpectedCanonicalBaseUrl = new(
      Environment.GetEnvironmentVariable("SEO_BASE_URL") ?? DefaultSeoBaseUrl);

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task PublicPages_HaveCanonicalMetadataAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      "channels/testing/",
      "learningpath/",
      "authors/abdul-rahman/",
      "about/",
      "privacy/",
      "disclaimer/",
      "career/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var title = await SeoTestHelpers.GetRequiredTitleAsync(Page);
      Assert.False(string.IsNullOrWhiteSpace(title));

      var description = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "meta[name='description']", "content");
      Assert.InRange(description.Length, 50, 320);

      var canonical = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "link[rel='canonical']", "href");
      SeoTestHelpers.AssertCanonicalUrl(canonical, ExpectedCanonicalBaseUrl, path);

      foreach (var hreflang in new[] { "en", "x-default" })
      {
        var href = await SeoTestHelpers.GetRequiredAttributeAsync(Page, $"link[rel='alternate'][hreflang='{hreflang}']", "href");
        Assert.Equal(canonical, href);
      }
    }
  }

  [Fact]
  public async Task AnalyticsPage_IsMarkedNoIndexAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "analytics/");

    var robots = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "meta[property='robots']", "content");
    Assert.Equal("noindex,follow", robots, ignoreCase: true);
  }

  [Fact]
  public async Task IndexablePages_DoNotDeclareNoIndexAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      "channels/testing/",
      "learningpath/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var robots = Page.Locator("meta[property='robots'], meta[name='robots']");
      if (await robots.CountAsync() > 0)
      {
        var content = await robots.First.GetAttributeAsync("content");
        Assert.DoesNotContain("noindex", content ?? string.Empty, StringComparison.OrdinalIgnoreCase);
      }
    }
  }

  [Fact]
  public async Task ArticlePage_HasSocialMetadataMatchingCanonicalUrlAsync()
  {
    const string path = "blogs/using-github-copilot-ai-for-navigating-new-codebase/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);

    var canonical = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "link[rel='canonical']", "href");
    await AssertSocialMetadataAsync(canonical, "article", "summary_large_image");
  }

  [Fact]
  public async Task ChannelPage_UsesWebsiteSocialMetadataAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "channels/testing/");

    var canonical = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "link[rel='canonical']", "href");
    await AssertSocialMetadataAsync(canonical, "website", "summary");
  }

  [Fact]
  public async Task BlogPage_HasValidArticleAndBreadcrumbStructuredDataAsync()
  {
    const string path = "blogs/using-github-copilot-ai-for-navigating-new-codebase/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);

    var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      var article = GetSchema(documents, "Article");
      foreach (var property in new[] { "headline", "description", "author", "datePublished", "dateModified", "image", "mainEntityOfPage" })
      {
        Assert.True(article.RootElement.TryGetProperty(property, out _), $"Article schema must contain '{property}'.");
      }

      var articleUrl = article.RootElement.GetProperty("mainEntityOfPage").GetProperty("@id").GetString();
      Assert.Equal(SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, path), articleUrl);

      var image = article.RootElement.GetProperty("image");
      Assert.True(image.GetProperty("width").GetInt32() > 0);
      Assert.True(image.GetProperty("height").GetInt32() > 0);

      AssertBreadcrumbSchema(GetSchema(documents, "BreadcrumbList"));
    }
    finally
    {
      foreach (var document in documents)
      {
        document.Dispose();
      }
    }
  }

  [Fact]
  public async Task ChannelAndLearningPathPages_HaveValidBreadcrumbStructuredDataAsync()
  {
    foreach (var path in new[] { "channels/testing/", "learningpath/" })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
      var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
      try
      {
        AssertBreadcrumbSchema(GetSchema(documents, "BreadcrumbList"));
        Assert.Contains(documents, document => document.RootElement.GetProperty("@type").GetString() == "WebSite");
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
  public async Task PublicWebsitePages_HaveValidWebsiteStructuredDataAsync()
  {
    foreach (var path in new[] { "", "about/", "authors/abdul-rahman/", "privacy/", "disclaimer/" })
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
  public async Task BlogPage_RendersVisibleBreadcrumbsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(
        Page,
        WebBaseUrl,
        "blogs/using-github-copilot-ai-for-navigating-new-codebase/");

    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    await breadcrumb.WaitForAsync();
    Assert.Equal(1, await breadcrumb.CountAsync());
    Assert.Equal(2, await breadcrumb.Locator("a").CountAsync());
    await Expect(breadcrumb.Locator("a").Nth(0)).ToHaveTextAsync("Home");
    await Expect(breadcrumb.Locator("a").Nth(1)).ToHaveTextAsync("AI");
    await Expect(breadcrumb.Locator("li[aria-current='page']"))
        .ToContainTextAsync("Using GitHub Copilot AI for Navigating New Codebase");
  }

  [Fact]
  public async Task ChannelAndLearningPathPages_RenderVisibleBreadcrumbsAsync()
  {
    foreach (var (path, currentTitle) in new[]
    {
      ("channels/testing/", "Testing"),
      ("learningpath/", ".NET Tutorial Learning Paths | C#, LINQ and Blazor"),
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
      await breadcrumb.WaitForAsync();
      Assert.Equal(1, await breadcrumb.CountAsync());
      Assert.Equal(1, await breadcrumb.Locator("a").CountAsync());
      await Expect(breadcrumb.Locator("a")).ToHaveTextAsync("Home");
      await Expect(breadcrumb.Locator("li[aria-current='page']")).ToHaveTextAsync(currentTitle);
    }
  }

  [Fact]
  public async Task Breadcrumbs_RemainVisibleOnMobileAsync()
  {
    await Page.SetViewportSizeAsync(390, 844);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "channels/testing/");

    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    await Expect(breadcrumb).ToBeVisibleAsync();
    var boundingBox = await breadcrumb.BoundingBoxAsync();
    Assert.NotNull(boundingBox);
    Assert.True(boundingBox.Width <= 390);
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

  [Fact]
  public async Task RepresentativePages_ImagesHaveAltTextAndDimensionsAsync()
  {
    foreach (var path in new[]
    {
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      "channels/testing/",
      "learningpath/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var images = Page.Locator("img");
      await images.First.WaitForAsync(new() { State = WaitForSelectorState.Attached });
      Assert.True(await images.CountAsync() > 0, $"Expected images on {path}.");

      for (var index = 0; index < await images.CountAsync(); index++)
      {
        var image = images.Nth(index);
        var alt = await image.GetAttributeAsync("alt");
        var source = await image.GetAttributeAsync("src");
        Assert.False(string.IsNullOrWhiteSpace(alt), $"Image {index} ({source}) on {path} must have alt text.");

        var width = await image.GetAttributeAsync("width");
        var height = await image.GetAttributeAsync("height");
        Assert.True(int.TryParse(width, out var parsedWidth) && parsedWidth > 0, $"Image {index} ({source}) on {path} must have a positive width.");
        Assert.True(int.TryParse(height, out var parsedHeight) && parsedHeight > 0, $"Image {index} ({source}) on {path} must have a positive height.");
      }
    }
  }

  [Fact]
  public async Task RepresentativePages_UseCanonicalInternalLinkFormatsAsync()
  {
    foreach (var path in new[]
    {
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      "channels/testing/",
      "learningpath/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var links = Page.Locator("a[href]");

      for (var index = 0; index < await links.CountAsync(); index++)
      {
        var href = await links.Nth(index).GetAttributeAsync("href");
        if (string.IsNullOrWhiteSpace(href) || href.StartsWith("#", StringComparison.Ordinal))
        {
          continue;
        }

        var uri = new Uri(new Uri(SeoTestHelpers.ExpectedUrl(WebBaseUrl, path)), href);
        if (!string.Equals(uri.Host, WebBaseUrl.Host, StringComparison.OrdinalIgnoreCase))
        {
          continue;
        }

        Assert.Empty(uri.Query);
        if (uri.AbsolutePath.StartsWith("/blogs/", StringComparison.OrdinalIgnoreCase)
            || uri.AbsolutePath.StartsWith("/channels/", StringComparison.OrdinalIgnoreCase)
            || uri.AbsolutePath.StartsWith("/authors/", StringComparison.OrdinalIgnoreCase))
        {
          Assert.EndsWith("/", uri.AbsolutePath, StringComparison.Ordinal);
          Assert.DoesNotContain(" ", uri.AbsolutePath, StringComparison.Ordinal);
        }
      }
    }
  }

  [Fact]
  public async Task CanonicalRoutes_Return200WithoutRedirectsAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      "channels/testing/",
      "learningpath/",
    })
    {
      var expectedUrl = SeoTestHelpers.ExpectedUrl(WebBaseUrl, path);
      var response = await Page.GotoAsync(expectedUrl);
      Assert.NotNull(response);
      Assert.Equal(200, response.Status);
      Assert.Equal(expectedUrl, response.Url);
    }
  }

  private static JsonDocument GetSchema(IEnumerable<JsonDocument> documents, string type)
  {
    var document = documents.SingleOrDefault(document =>
        document.RootElement.TryGetProperty("@type", out var schemaType)
        && schemaType.GetString() == type);
    Assert.NotNull(document);
    return document;
  }

  private async Task AssertSocialMetadataAsync(string canonical, string expectedOpenGraphType, string expectedTwitterCard)
  {
    Assert.Equal(1, await Page.Locator("meta[property='og:type']").CountAsync());
    Assert.Equal(expectedOpenGraphType, await Page.Locator("meta[property='og:type']").GetAttributeAsync("content"));

    foreach (var property in new[] { "og:title", "og:description", "twitter:title", "twitter:description" })
    {
      var content = await SeoTestHelpers.GetRequiredAttributeAsync(Page, $"meta[property='{property}']", "content");
      Assert.False(string.IsNullOrWhiteSpace(content));
    }

    foreach (var property in new[] { "og:url", "twitter:url" })
    {
      var url = await SeoTestHelpers.GetRequiredAttributeAsync(Page, $"meta[property='{property}']", "content");
      Assert.Equal(canonical, url);
    }

    foreach (var property in new[] { "og:image", "twitter:image" })
    {
      var image = await SeoTestHelpers.GetRequiredAttributeAsync(Page, $"meta[property='{property}']", "content");
      Assert.StartsWith("https://", image, StringComparison.Ordinal);
    }

    var twitterCard = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "meta[property='twitter:card']", "content");
    Assert.Equal(expectedTwitterCard, twitterCard);
  }

  private void AssertBreadcrumbSchema(JsonDocument document)
  {
    var items = document.RootElement.GetProperty("itemListElement");
    Assert.True(items.GetArrayLength() >= 2);

    for (var index = 0; index < items.GetArrayLength(); index++)
    {
      var item = items[index];
      Assert.Equal(index + 1, item.GetProperty("position").GetInt32());
      Assert.False(string.IsNullOrWhiteSpace(item.GetProperty("name").GetString()));

      var url = item.GetProperty("item").GetString();
      Assert.StartsWith(ExpectedCanonicalBaseUrl.AbsoluteUri, url, StringComparison.Ordinal);
    }
  }
}
