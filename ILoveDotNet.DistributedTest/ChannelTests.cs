using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class ChannelTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task ChannelPage_HasMetadataPublishedArticlesAndFilteredLearningSequenceAsync()
  {
    const string path = "channels/linq/";
    var response = await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    Assert.Equal(SeoTestHelpers.ExpectedUrl(WebBaseUrl, path), response.Url);
    var records = new SharedModels.TableOfContents().Contents.Where(content => content.Channel == "LINQ").ToArray();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("LINQ");
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    var canonical = await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);
    await SeoTestHelpers.AssertSocialMetadataAsync(Page, canonical, "website", "summary");
    foreach (var content in await Page.Locator("meta[property='robots'], meta[name='robots']")
        .EvaluateAllAsync<string[]>("elements => elements.map(element => element.content)"))
    {
      Assert.DoesNotContain("noindex", content, StringComparison.OrdinalIgnoreCase);
    }
    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    await Expect(breadcrumb).ToHaveCountAsync(1);
    await Expect(breadcrumb).ToBeVisibleAsync();
    await Expect(breadcrumb.Locator("a")).ToHaveTextAsync(["Home"]);
    await Expect(breadcrumb.Locator("li[aria-current='page']")).ToHaveTextAsync("LINQ");
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
    var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      SeoTestHelpers.AssertBreadcrumbSchema(SeoTestHelpers.GetSchema(documents, "BreadcrumbList"));
      SeoTestHelpers.GetSchema(documents, "WebSite");
    }
    finally
    {
      foreach (var document in documents)
      {
        document.Dispose();
      }
    }
    await SeoTestHelpers.AssertImageAttributesAsync(Page, path);
    await SeoTestHelpers.AssertCanonicalInternalLinksAsync(Page, WebBaseUrl, path);
    var results = Page.GetByRole(AriaRole.Region, new() { Name = "Latest articles", Exact = true });
    await Expect(results.Locator("article")).ToHaveCountAsync(records.Length);
    await Expect(results.Locator("h3")).ToHaveTextAsync(records.OrderByDescending(content => content.ModifiedOn).Select(content => content.Title).ToArray());
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Latest articles", Exact = true })).ToHaveAttributeAsync("aria-pressed", "true");
    await Page.GetByRole(AriaRole.Link, new() { Name = "Start learning", Exact = false }).ClickAsync();
    Assert.Equal(WebBaseUrl.Authority, new Uri(Page.Url).Authority);
    results = Page.GetByRole(AriaRole.Region, new() { Name = "Learning sequence", Exact = true });
    await Expect(Page.Locator("#channel-results")).ToBeInViewportAsync();
    await Expect(results.Locator("li")).ToHaveCountAsync(records.Length);
    var sequence = records.OrderBy(content => content.Order).ToArray();
    await Expect(results.Locator("h3")).ToHaveTextAsync(sequence.Select(content => content.Title).ToArray());
    var search = Page.GetByLabel("Search LINQ tutorials", new() { Exact = true });
    var selected = sequence[^1];
    await search.FillAsync(selected.Title);
    await Expect(results.Locator("li")).ToHaveCountAsync(1);
    await Expect(results.Locator("li > span")).ToHaveTextAsync($"Step {sequence.Length}");
    await search.FillAsync("not-a-real-tutorial");
    await Expect(Page.GetByRole(AriaRole.Status).Filter(new() { HasText = "No matching tutorials." })).ToBeVisibleAsync();
    await Page.GetByRole(AriaRole.Button, new() { Name = "Clear search", Exact = true }).ClickAsync();
    await Expect(results.Locator("li")).ToHaveCountAsync(records.Length);
    var latest = Page.GetByRole(AriaRole.Button, new() { Name = "Latest articles", Exact = true });
    await latest.FocusAsync();
    await Page.Keyboard.PressAsync("Enter");
    await Expect(latest).ToHaveAttributeAsync("aria-pressed", "true");
    await Expect(Page.Locator("#channel-results")).ToHaveTextAsync("Latest articles");
    await Expect(Page.Locator("#channel-results + div article")).ToHaveCountAsync(records.Length);
  }

  [Fact]
  public async Task ChannelPage_UpdatesRoutesFitsMobileLayoutsAndRecoversFromUnknownChannelsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "channels/dependency-injection/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Dependency Injection");
    var canonicalBaseUrl = SeoTestHelpers.GetExpectedCanonicalBaseUrl();
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{canonicalBaseUrl}channels/dependency-injection/");
    await Expect(Page.Locator("img").First).ToHaveAttributeAsync("src", "image/icons/dependency injection.webp");
    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    foreach (var width in new[] { 320, 375, 390, 414, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        await Expect(breadcrumb).ToBeVisibleAsync();
        var boundingBox = await breadcrumb.BoundingBoxAsync();
        Assert.NotNull(boundingBox);
        Assert.True(boundingBox.Width <= width);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Channel page must fit at {width}px in {(dark ? "dark" : "light")} mode.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Learning sequence", Exact = true }).ClickAsync();
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
        await Page.GetByRole(AriaRole.Button, new() { Name = "Latest articles", Exact = true }).ClickAsync();
      }
    }
    await Page.GetByLabel("Search Dependency Injection tutorials", new() { Exact = true }).FillAsync("not-a-real-tutorial");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Learning sequence", Exact = true }).ClickAsync();
    await Page.EvaluateAsync("() => Blazor.navigateTo('/channels/web-api/')");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Web API");
    await Expect(Page.GetByLabel("Search Web API tutorials", new() { Exact = true })).ToHaveValueAsync("");
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Latest articles", Exact = true })).ToHaveAttributeAsync("aria-pressed", "true");
    await Page.WaitForFunctionAsync("() => { const image = document.querySelector('img'); return image.complete && image.naturalWidth > 0; }");
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{canonicalBaseUrl}channels/web-api/");
    await Page.EvaluateAsync("() => Blazor.navigateTo('/channels/efcore/')");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("EFCore");
    var emptyChannel = new SharedModels.TableOfContents().AllContents
        .GroupBy(content => content.Channel).FirstOrDefault(channel => channel.All(content => content.ModifiedOn.Date > DateTime.Today));
    if (emptyChannel is not null)
    {
      await Page.EvaluateAsync("url => Blazor.navigateTo(url)", $"/channels/{emptyChannel.Key.ToLowerInvariant().Replace(" ", "-")}/");
      await Expect(Page.GetByRole(AriaRole.Status).Filter(new() { HasText = "No articles have been published" })).ToBeVisibleAsync();
    }
    await Page.EvaluateAsync("() => Blazor.navigateTo('/channels/not-a-real-channel/')");
    await Expect(Page.GetByRole(AriaRole.Status).Filter(new() { HasText = "This channel was not found." })).ToBeVisibleAsync();
    await Page.GetByRole(AriaRole.Link, new() { Name = "Browse all learning paths", Exact = false }).ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync(".NET Tutorial Learning Paths");
  }
}
