using System.Text.Json;
using Microsoft.Playwright;

namespace ILoveDotNet.DistributedTest.Utils;

public static class SeoTestHelpers
{
  public static Uri GetExpectedCanonicalBaseUrl()
  {
#if DEBUG
    const string defaultSeoBaseUrl = "https://localhost:7176/";
#else
    const string defaultSeoBaseUrl = "https://ilovedotnet.org/";
#endif
    return new Uri(Environment.GetEnvironmentVariable("SEO_BASE_URL") ?? defaultSeoBaseUrl);
  }

  public static async Task<IResponse> GotoSuccessfulPageAsync(IPage page, Uri baseUrl, string path)
  {
    var response = await page.GotoAsync(new Uri(baseUrl, path.TrimStart('/')).AbsoluteUri);
    Assert.NotNull(response);
    Assert.Equal(200, response.Status);
    return response;
  }

  public static async Task<string> AssertCanonicalMetadataAsync(IPage page, string path)
  {
    await GetRequiredTitleAsync(page);
    var description = await GetRequiredAttributeAsync(page, "meta[name='description']", "content");
    Assert.InRange(description.Length, 50, 320);
    var canonical = await GetRequiredAttributeAsync(page, "link[rel='canonical']", "href");
    AssertCanonicalUrl(canonical, GetExpectedCanonicalBaseUrl(), path);
    foreach (var hreflang in new[] { "en", "x-default" })
    {
      var href = await GetRequiredAttributeAsync(page, $"link[rel='alternate'][hreflang='{hreflang}']", "href");
      Assert.Equal(canonical, href);
    }
    return canonical;
  }

  public static async Task AssertWebsiteMetadataAsync(IPage page, string path)
  {
    var canonical = await AssertCanonicalMetadataAsync(page, path);
    await Assertions.Expect(page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(1);
    var documents = await GetJsonLdDocumentsAsync(page);
    try
    {
      var website = Assert.Single(documents).RootElement;
      Assert.Equal("WebSite", website.GetProperty("@type").GetString());
      Assert.Equal(canonical, website.GetProperty("url").GetString());
      Assert.Equal(canonical, website.GetProperty("mainEntityOfPage").GetProperty("@id").GetString());
      Assert.Equal("Organization", website.GetProperty("publisher").GetProperty("@type").GetString());
    }
    finally
    {
      foreach (var document in documents)
      {
        document.Dispose();
      }
    }
  }

  public static async Task<string> GetRequiredAttributeAsync(IPage page, string selector, string attribute)
  {
    var locator = page.Locator(selector);
    await locator.WaitForAsync(new() { State = WaitForSelectorState.Attached });
    Assert.Equal(1, await locator.CountAsync());

    var value = await locator.GetAttributeAsync(attribute);
    Assert.False(string.IsNullOrWhiteSpace(value), $"Expected {selector} to have a non-empty {attribute} attribute.");
    return value;
  }

  public static async Task<string> GetRequiredTitleAsync(IPage page)
  {
    var title = await page.TitleAsync();
    Assert.False(string.IsNullOrWhiteSpace(title), "Expected the page title to be non-empty.");
    return title;
  }

  public static async Task<JsonDocument[]> GetJsonLdDocumentsAsync(IPage page)
  {
    var scripts = page.Locator("script[type='application/ld+json']");
    var documents = new List<JsonDocument>();

    for (var index = 0; index < await scripts.CountAsync(); index++)
    {
      var json = await scripts.Nth(index).TextContentAsync();
      Assert.False(string.IsNullOrWhiteSpace(json), $"JSON-LD script {index} must not be empty.");
      Assert.DoesNotContain("&quot;", json, StringComparison.Ordinal);
      Assert.DoesNotContain("&lt;", json, StringComparison.Ordinal);
      Assert.DoesNotContain("&gt;", json, StringComparison.Ordinal);
      documents.Add(JsonDocument.Parse(json));
    }

    return [.. documents];
  }

  public static string ExpectedUrl(Uri baseUrl, string path)
    => new Uri(baseUrl, path.TrimStart('/')).AbsoluteUri;

  public static async Task AssertResponsiveThumbnailCardAsync(IPage page, bool clamped)
  {
    var card = page.Locator("[data-thumbnail]").First;
    await Assertions.Expect(card).ToBeVisibleAsync();
    await Assertions.Expect(card.Locator("main, aside, article")).ToHaveCountAsync(0);
    await Assertions.Expect(card.Locator("h3")).ToHaveCountAsync(1);
    await Assertions.Expect(card.Locator("time")).ToHaveAttributeAsync("title", new System.Text.RegularExpressions.Regex(@"^\d{1,2} \w+ \d{4}$"));
    await Assertions.Expect(card.Locator("a[aria-label^='Explore']")).ToHaveAttributeAsync("href",
        new System.Text.RegularExpressions.Regex(@"^/channels/[^/]+/$"));
    foreach (var dark in new[] { false, true })
    {
      await page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
      foreach (var width in new[] { 320, 375, 768, 1280, 1440 })
      {
        await page.SetViewportSizeAsync(width, 900);
        Assert.True(await page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
        Assert.True(await card.EvaluateAsync<bool>(
            "element => element.scrollWidth <= element.clientWidth && getComputedStyle(element).transform === 'none'"));
        Assert.True(await card.Locator("header img").EvaluateAsync<bool>(
            "image => { const bounds = image.getBoundingClientRect(); return Math.abs(bounds.width / bounds.height - 1265 / 827) < 0.02; }"));
        Assert.Equal(clamped ? "2" : "none", await card.Locator("h3").EvaluateAsync<string>(
            "element => getComputedStyle(element).webkitLineClamp"));
        await Assertions.Expect(card.Locator("div > a > img")).ToHaveCSSAsync("width", "40px");
      }
    }
    await card.Locator("header a").FocusAsync();
    await Assertions.Expect(card.Locator("header a")).ToBeFocusedAsync();
    await Assertions.Expect(card.Locator("header a")).ToHaveCSSAsync("outline-style", "solid");
  }

  public static void AssertCanonicalUrl(string actualUrl, Uri baseUrl, string expectedPath)
  {
    Assert.Equal(ExpectedUrl(baseUrl, expectedPath), actualUrl);
    Assert.StartsWith(baseUrl.Scheme + "://", actualUrl, StringComparison.Ordinal);
    Assert.EndsWith("/", new Uri(actualUrl).AbsolutePath, StringComparison.Ordinal);
  }

  public static JsonDocument GetSchema(IEnumerable<JsonDocument> documents, string type)
  {
    var document = documents.SingleOrDefault(document =>
        document.RootElement.TryGetProperty("@type", out var schemaType)
        && schemaType.GetString() == type);
    Assert.NotNull(document);
    return document;
  }

  public static async Task AssertSocialMetadataAsync(IPage page, string canonical, string expectedOpenGraphType, string expectedTwitterCard)
  {
    var openGraphType = await GetRequiredAttributeAsync(page, "meta[property='og:type']", "content");
    Assert.Equal(expectedOpenGraphType, openGraphType);
    foreach (var property in new[] { "og:title", "og:description", "twitter:title", "twitter:description" })
    {
      await GetRequiredAttributeAsync(page, $"meta[property='{property}']", "content");
    }
    foreach (var property in new[] { "og:url", "twitter:url" })
    {
      var url = await GetRequiredAttributeAsync(page, $"meta[property='{property}']", "content");
      Assert.Equal(canonical, url);
    }
    foreach (var property in new[] { "og:image", "twitter:image" })
    {
      var image = await GetRequiredAttributeAsync(page, $"meta[property='{property}']", "content");
      Assert.StartsWith("https://", image, StringComparison.Ordinal);
    }
    var twitterCard = await GetRequiredAttributeAsync(page, "meta[property='twitter:card']", "content");
    Assert.Equal(expectedTwitterCard, twitterCard);
  }

  public static void AssertBreadcrumbSchema(JsonDocument document)
  {
    var items = document.RootElement.GetProperty("itemListElement");
    Assert.True(items.GetArrayLength() >= 2);
    var baseUrl = GetExpectedCanonicalBaseUrl();
    for (var index = 0; index < items.GetArrayLength(); index++)
    {
      var item = items[index];
      Assert.Equal(index + 1, item.GetProperty("position").GetInt32());
      Assert.False(string.IsNullOrWhiteSpace(item.GetProperty("name").GetString()));
      Assert.StartsWith(baseUrl.AbsoluteUri, item.GetProperty("item").GetString(), StringComparison.Ordinal);
    }
  }
}
