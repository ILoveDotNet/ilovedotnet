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

  public static async Task AssertWebsiteMetadataAsync(IPage page, string path)
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

  public static void AssertCanonicalUrl(string actualUrl, Uri baseUrl, string expectedPath)
  {
    Assert.Equal(ExpectedUrl(baseUrl, expectedPath), actualUrl);
    Assert.StartsWith(baseUrl.Scheme + "://", actualUrl, StringComparison.Ordinal);
    Assert.EndsWith("/", new Uri(actualUrl).AbsolutePath, StringComparison.Ordinal);
  }
}
