using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningPathTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task LearningPathPage_HasMetadataPublishedSequencesAndWorkingSearchAsync()
  {
    const string path = "learningpath/";
    var response = await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    Assert.Equal(SeoTestHelpers.ExpectedUrl(WebBaseUrl, path), response.Url);
    await Expect(Page.Locator("h1")).ToHaveTextAsync(".NET Tutorial Learning Paths");
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);
    foreach (var content in await Page.Locator("meta[property='robots'], meta[name='robots']")
        .EvaluateAllAsync<string[]>("elements => elements.map(element => element.content)"))
    {
      Assert.DoesNotContain("noindex", content, StringComparison.OrdinalIgnoreCase);
    }
    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    await Expect(breadcrumb).ToHaveCountAsync(1);
    await Expect(breadcrumb).ToBeVisibleAsync();
    await Expect(breadcrumb.Locator("a")).ToHaveTextAsync(["Home"]);
    await Expect(breadcrumb.Locator("li[aria-current='page']")).ToHaveTextAsync(".NET Tutorial Learning Paths");
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

    var published = new SharedModels.TableOfContents().Contents;
    var channels = published.GroupBy(content => content.Channel).OrderBy(channel => channel.Key).ToArray();
    var shelves = Page.Locator("section[aria-labelledby^='path-']");
    await Expect(shelves).ToHaveCountAsync(channels.Length);
    for (var index = 0; index < channels.Length; index++)
    {
      var shelf = shelves.Nth(index);
      var expected = channels[index].OrderBy(content => content.Order).ToArray();
      await Expect(shelf.Locator("h2")).ToHaveTextAsync(channels[index].Key);
      await Expect(shelf.Locator("header p")).ToHaveTextAsync($"{expected.Length} published articles");
      await Expect(shelf.Locator("li")).ToHaveCountAsync(expected.Length);
      await Expect(shelf.Locator("li h3")).ToHaveTextAsync(expected.Select(content => content.Title).ToArray());
      await Expect(shelf.Locator("li span")).ToHaveTextAsync(
          Enumerable.Range(1, expected.Length).Select(step => $"Step {step}").ToArray());
      var hrefs = await shelf.Locator("li a").EvaluateAllAsync<string[]>(
          "links => links.map(link => link.getAttribute('href'))");
      Assert.Equal(expected.Select(content => content.ContentUrl).ToArray(), hrefs);
    }
    await SeoTestHelpers.AssertImageAttributesAsync(Page, path);
    await SeoTestHelpers.AssertCanonicalInternalLinksAsync(Page, WebBaseUrl, path);
    await Page.GetByText("Browse topics and article counts", new() { Exact = true }).ClickAsync();
    var topics = Page.GetByRole(AriaRole.Navigation, new() { Name = "Learning path topics" }).Locator("a");
    await Expect(topics).ToHaveCountAsync(channels.Length);
    var target = await topics.Last.GetAttributeAsync("href");
    Assert.NotNull(target);
    await topics.Last.ClickAsync();
    await Expect(Page.Locator(new Uri(new Uri(Page.Url), target).Fragment)).ToBeInViewportAsync();
    var search = Page.GetByLabel("Search topics", new() { Exact = true });
    await search.FillAsync("  lInQ ");
    await Expect(shelves).ToHaveCountAsync(1);
    await Expect(shelves.Locator("h2")).ToHaveTextAsync("LINQ");
    await Expect(topics).ToHaveCountAsync(1);
    await search.FillAsync("not-a-real-topic");
    await Expect(shelves).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Status).Filter(new() { HasText = "No matching topics." })).ToBeVisibleAsync();
    await search.FillAsync("");
    await Expect(shelves).ToHaveCountAsync(channels.Length);
    var lastCard = shelves.Last.Locator("li a").Last;
    var banner = Page.GetByRole(AriaRole.Link, new() { Name = "Start preparing for your C# and .NET interview", Exact = true });
    Assert.True(await banner.EvaluateAsync<bool>("banner => Boolean([...document.querySelectorAll('section[aria-labelledby^=\"path-\"]')].at(-1).compareDocumentPosition(banner) & Node.DOCUMENT_POSITION_FOLLOWING)"));
    await lastCard.ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveCountAsync(1);
    Assert.Contains("/blogs/", Page.Url);
  }

  [Fact]
  public async Task LearningPathPage_KeepsScrollingInsideResponsiveShelvesAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "learningpath/");
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    var shelf = Page.GetByRole(AriaRole.Region, new() { Name = "Web API learning sequence", Exact = true });
    await Expect(shelf).ToBeVisibleAsync();
    foreach (var width in new[] { 320, 375, 390, 414, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Learning paths should not widen the page at {width}px in {(dark ? "dark" : "light")} mode.");
        Assert.True(await shelf.EvaluateAsync<bool>("element => element.scrollWidth > element.clientWidth"));
      }
    }
    await Page.SetViewportSizeAsync(390, 844);
    var firstCard = await shelf.Locator("li").First.BoundingBoxAsync();
    var box = await shelf.BoundingBoxAsync();
    Assert.NotNull(firstCard);
    Assert.NotNull(box);
    Assert.InRange(firstCard.Width / box.Width, 0.75, 0.85);
    await shelf.Locator("li a").Last.FocusAsync();
    await Expect(shelf.Locator("li a").Last).ToBeInViewportAsync();
    Assert.True(await shelf.EvaluateAsync<bool>("element => element.scrollLeft > 0"));
    var channel = Page.GetByRole(AriaRole.Link, new() { Name = "View Web API channel", Exact = true });
    await channel.ClickAsync();
    Assert.Equal("/channels/web-api/", new Uri(Page.Url).AbsolutePath);
    await Expect(Page.Locator("h1")).ToHaveCountAsync(1);
  }
}
