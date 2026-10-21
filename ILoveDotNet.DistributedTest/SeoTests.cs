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
  public async Task ArticleLayout_PlacesSidebarAdBeforeRecommendationsWithoutNestedScrollingAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
    await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
    var sidebar = Page.Locator("aside[aria-label='Article resources']");
    var ad = sidebar.GetByRole(AriaRole.Region, new() { Name = "Advertisement", Exact = true });
    await Expect(ad).ToHaveCountAsync(1);
    await Expect(ad).ToContainTextAsync("Ad Space");
    await Expect(sidebar.Locator("section[aria-labelledby='related-articles'] > section[aria-label='Advertisement']")).ToHaveCountAsync(1);
    await Expect(sidebar.Locator(":scope > section")).ToHaveAttributeAsync("aria-labelledby", "related-articles");
    foreach (var width in new[] { 320, 375, 768, 1280, 1440 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      Assert.True(await sidebar.EvaluateAsync<bool>(
          "element => { const style = getComputedStyle(element); return style.position === 'static' && style.overflowY === 'visible' && element.scrollHeight <= element.clientHeight + 1; }"));
      Assert.True(await ad.EvaluateAsync<bool>(
          "element => element.getBoundingClientRect().bottom <= document.querySelector('#related-articles').getBoundingClientRect().top"));
      Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
      if (width >= 1280)
      {
        Assert.True(await sidebar.Locator(":scope > div").First.EvaluateAsync<bool>(
            "element => element.getBoundingClientRect().bottom <= element.nextElementSibling.getBoundingClientRect().top"));
        Assert.True(await sidebar.Locator(":scope > div").First.EvaluateAsync<bool>(
            "element => element.getBoundingClientRect().height <= 250"));
      }
      else
      {
        Assert.True(await sidebar.EvaluateAsync<bool>(
            "element => element.getBoundingClientRect().top >= document.querySelector('article[aria-labelledby=\"article-title\"]').getBoundingClientRect().bottom"));
      }
    }
    await Expect(Page.Locator("section[aria-label='Article body']")).ToContainTextAsync("Ad Space");
  }

  [Fact]
  public async Task ArticleLayout_RestoresStackedCardsAndContainerBoundedStickyAdAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
    await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
    var ad = Page.GetByRole(AriaRole.Region, new() { Name = "Advertisement", Exact = true });
    foreach (var width in new[] { 320, 375, 768, 1024, 1280, 1440 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      await Page.EvaluateAsync("() => { document.documentElement.style.scrollBehavior = 'auto'; window.scrollTo(0, 0); }");
      await Expect(ad).ToHaveCSSAsync("position", width >= 1024 ? "sticky" : "static");
      var card = Page.Locator("section[aria-labelledby='related-articles'] > div[aria-live] > div").First;
      Assert.True(await card.EvaluateAsync<bool>("element => { const poster = element.querySelector('header img').getBoundingClientRect(); const title = element.querySelector('h3'); const bounds = title.getBoundingClientRect(); return Math.abs(poster.width - element.getBoundingClientRect().width) < 1 && bounds.top >= poster.bottom && getComputedStyle(title).webkitLineClamp === 'none'; }"));
      await Expect(card.Locator(":scope > section")).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("md:hover:scale-110"));
      await Expect(card.Locator("img")).ToHaveCountAsync(2);
      await Expect(card.Locator("time")).ToHaveAttributeAsync("datetime", new System.Text.RegularExpressions.Regex(@"^\d{4}-\d{2}-\d{2}$"));
      Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
      if (width >= 1024)
      {
        await ad.EvaluateAsync("element => window.scrollTo(0, element.getBoundingClientRect().top + scrollY + 100)");
        await Page.WaitForFunctionAsync("() => Math.abs(document.querySelector('section[aria-label=\"Advertisement\"]').getBoundingClientRect().top - 56) < 1");
        await Page.EvaluateAsync("() => window.scrollTo(0, document.documentElement.scrollHeight)");
        Assert.True(await ad.EvaluateAsync<bool>(
            "element => { const ad = element.getBoundingClientRect(); const section = element.parentElement.getBoundingClientRect(); return ad.bottom <= section.bottom + 1; }"));
      }
    }
    await Expect(Page.Locator("[data-sidebar-ad-track]")).ToHaveCountAsync(0);
    await Page.EvaluateAsync("() => Blazor.navigateTo('/learningpath/')");
    await Expect(Page.Locator("section[aria-labelledby='related-articles']")).ToHaveCountAsync(0);
  }

  [Fact]
  public async Task ArticleLayout_ShowsReaderFirstHeaderAndWorkingAnchorsAsync()
  {
    const string slug = "dependency-injection-lifetimes-in-dotnet";
    var contents = new SharedModels.TableOfContents().Contents;
    var metadata = contents.Single(content => content.Slug == slug);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, $"blogs/{slug}/?source=test");
    await Expect(Page.Locator("#article-title")).ToHaveTextAsync(metadata.Title);
    Assert.True(await Page.Locator("#article-title").EvaluateAsync<bool>(
        "title => title.getBoundingClientRect().top < title.closest('header').querySelector('figure').getBoundingClientRect().top"));
    await Expect(Page.Locator("article[aria-labelledby='article-title'] > header time")).ToHaveTextAsync("Updated 13 Oct 2024");
    await Expect(Page.Locator("article[aria-labelledby='article-title'] > header > a")).ToHaveTextAsync(
        $"{metadata.Channel} · {contents.Count(content => content.Channel == metadata.Channel)} published articles");
    await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Edit on GitHub", Exact = false })).ToHaveAttributeAsync("href",
        "https://github.com/ILoveDotNet/ilovedotnet/blob/main/DependencyInjectionDemoComponents/Pages/Blogs/Lifetimes.razor");
    await Page.GetByRole(AriaRole.Link, new() { Name = "Implementation", Exact = true }).ClickAsync();
    await Expect(Page.Locator("#how")).ToBeInViewportAsync();
    Assert.Equal(WebBaseUrl.Authority, new Uri(Page.Url).Authority);
    Assert.True(await Page.Locator("#how").EvaluateAsync<bool>("heading => heading.getBoundingClientRect().top >= 56"));
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{ExpectedCanonicalBaseUrl}blogs/{slug}/");
    await Expect(Page).ToHaveTitleAsync($"{metadata.Title} - I ❤️ DotNet");
    var navigation = Page.GetByRole(AriaRole.Navigation, new() { Name = "Continue this learning path", Exact = true });
    var ordered = contents.Where(content => content.Channel == metadata.Channel).OrderBy(content => content.Order).ToArray();
    var index = Array.FindIndex(ordered, content => content.Slug == slug);
    await Expect(navigation.Locator("a[rel='prev']")).ToHaveAttributeAsync("href", $"/{ordered[index - 1].ContentUrl}");
    await Expect(navigation.Locator("a[rel='next']")).ToHaveAttributeAsync("href", $"/{ordered[index + 1].ContentUrl}");
    await navigation.Locator("a[rel='prev']").ClickAsync();
    await Expect(Page.Locator("#article-title")).ToHaveTextAsync(ordered[index - 1].Title);
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{ExpectedCanonicalBaseUrl}{ordered[index - 1].ContentUrl}");
    await Expect(navigation.Locator("a[rel='prev']")).ToHaveCountAsync(0);
    var share = Page.GetByRole(AriaRole.Link, new() { Name = "Share via WhatsApp", Exact = false });
    var href = (await share.GetAttributeAsync("href"))!;
    Assert.Equal($"{ordered[index - 1].Title} {ExpectedCanonicalBaseUrl}{ordered[index - 1].ContentUrl}",
        Uri.UnescapeDataString(href[(href.IndexOf("text=", StringComparison.Ordinal) + 5)..]));
    await Expect(Page.Locator("[data-share-badge-host]")).ToHaveCountAsync(0);
    await Expect(Page.Locator("meta[property='og:image']")).ToHaveCountAsync(1);
    await Expect(Page.Locator("meta[property='twitter:image']")).ToHaveCountAsync(1);
  }

  [Fact]
  public async Task ArticleLayout_PaginatesOnlyRelatedPublishedArticlesAndHandlesSingleArticleAsync()
  {
    var table = new SharedModels.TableOfContents();
    var metadata = table.Contents.First(content => content.Channel == "LINQ");
    var expected = table.GetRelatedContents(metadata.Slug, int.MaxValue);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, metadata.ContentUrl);
    var related = Page.GetByRole(AriaRole.Region, new() { Name = "Related articles", Exact = true });
    await Expect(related.Locator("article")).ToHaveCountAsync(Math.Min(6, expected.Count));
    var count = 6;
    while (count < expected.Count)
    {
      await related.GetByRole(AriaRole.Button, new() { Name = "Show more related articles", Exact = true }).ClickAsync();
      count += 6;
      await Expect(related.Locator("article")).ToHaveCountAsync(Math.Min(count, expected.Count));
    }
    await Expect(related.Locator("h3")).ToHaveTextAsync(expected.Select(content => content.Title).ToArray());
    await Expect(related.GetByRole(AriaRole.Button)).ToHaveCountAsync(0);
    await Expect(related.Locator($"a[href='/{metadata.ContentUrl}']")).ToHaveCountAsync(0);
    var last = table.Contents.Where(content => content.Channel == metadata.Channel).OrderBy(content => content.Order).Last();
    await Page.EvaluateAsync("url => Blazor.navigateTo(url)", $"/{last.ContentUrl}");
    await Expect(Page.Locator("#article-title")).ToHaveTextAsync(last.Title);
    await Expect(Page.Locator("nav[aria-label='Continue this learning path'] a[rel='next']")).ToHaveCountAsync(0);
    await Expect(related.Locator("article")).ToHaveCountAsync(6);
    var video = table.Contents.Single(content => content.Channel == "Talk");
    await Page.EvaluateAsync("url => Blazor.navigateTo(url)", $"/{video.ContentUrl}");
    await Expect(Page.Locator("#article-title")).ToHaveTextAsync(video.Title);
    await Expect(Page.Locator("header iframe")).ToHaveAttributeAsync("src", video.VideoUrl!);
    await Expect(related.Locator("article")).ToHaveCountAsync(0);
    await Expect(related.GetByRole(AriaRole.Link, new() { Name = "Browse learning paths", Exact = false })).ToBeVisibleAsync();
    await Expect(Page.Locator("nav[aria-label='Continue this learning path'] a[rel]")).ToHaveCountAsync(0);
  }

  [Fact]
  public async Task ArticleLayout_CopiesCodeAndReportsClipboardFailureAsync()
  {
    await Context.GrantPermissionsAsync(["clipboard-read", "clipboard-write"]);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
    var snippet = Page.Locator("div.not-prose").Filter(new() { Has = Page.Locator("pre") }).First;
    var button = snippet.GetByRole(AriaRole.Button, new() { Name = "Copy Razor code", Exact = true });
    await Expect(button).ToBeEnabledAsync();
    await button.ClickAsync();
    await Expect(snippet.GetByRole(AriaRole.Status)).ToHaveTextAsync("Copied to clipboard.");
    await Expect(button).ToHaveTextAsync("Copied");
    Assert.Equal(await snippet.Locator("code").TextContentAsync(), await Page.EvaluateAsync<string>("() => navigator.clipboard.readText()"));
    await Page.EvaluateAsync("() => Object.defineProperty(navigator.clipboard, 'writeText', { configurable: true, value: () => Promise.reject(new DOMException('Permission denied', 'NotAllowedError')) })");
    await button.ClickAsync();
    await Expect(snippet.GetByRole(AriaRole.Status)).ToHaveTextAsync("Copy failed. Select the code and copy it manually.");
    await Expect(snippet.GetByRole(AriaRole.Status)).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Region, new() { Name = "Live demo: Transient Service Dependency", Exact = true })).ToContainTextAsync("Transient");
    await Expect(snippet.Locator("code")).ToHaveAttributeAsync("data-highlighted", "yes");
  }

  [Fact]
  public async Task ArticleLayout_FitsResponsiveReadingAndCodeRegionsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
    await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
    foreach (var width in new[] { 320, 375, 390, 414, 768, 1280, 1440 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Article page must not scroll horizontally at {width}px in {(dark ? "dark" : "light")} mode.");
        if (width < 1280)
        {
          await Expect(Page.Locator("details")).Not.ToHaveAttributeAsync("open", "");
          await Page.Locator("details summary").ClickAsync();
          await Page.GetByRole(AriaRole.Link, new() { Name = "Implementation", Exact = true }).ClickAsync();
          await Expect(Page.Locator("#how")).ToBeInViewportAsync();
          await Page.Locator("details summary").ClickAsync();
        }
      }
    }
    await Page.SetViewportSizeAsync(320, 900);
    var code = Page.GetByRole(AriaRole.Region, new() { Name = "Razor code", Exact = true }).First;
    await code.FocusAsync();
    await Page.Keyboard.PressAsync("ArrowRight");
    await Page.WaitForFunctionAsync("() => document.querySelector('pre').scrollLeft > 0");
    Assert.True(await code.EvaluateAsync<bool>("element => element.scrollWidth > element.clientWidth"));
    foreach (var path in new[]
    {
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
      new SharedModels.TableOfContents().Contents.First(content => content.Channel == "Web API").ContentUrl
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
      Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
          $"Article {path} must fit at 320px.");
    }
  }

  [Fact]
  public async Task ChannelPage_ShowsPublishedArticlesAndFilteredLearningSequenceAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "channels/linq/");
    var records = new SharedModels.TableOfContents().Contents.Where(content => content.Channel == "LINQ").ToArray();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("LINQ");
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
  public async Task ChannelPage_UpdatesRoutesAndFitsMobileLayoutsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "channels/dependency-injection/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Dependency Injection");
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{ExpectedCanonicalBaseUrl}channels/dependency-injection/");
    await Expect(Page.Locator("img").First).ToHaveAttributeAsync("src", "image/icons/dependency injection.webp");
    foreach (var width in new[] { 320, 375, 390, 414, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
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
    await Expect(Page.Locator("link[rel='canonical']")).ToHaveAttributeAsync("href", $"{ExpectedCanonicalBaseUrl}channels/web-api/");
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

  [Fact]
  public async Task LearningPathPage_ShowsPublishedOrderedSequencesAndSearchAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "learningpath/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync(".NET Tutorial Learning Paths");
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
      for (var step = 0; step < expected.Length; step++)
      {
        var card = shelf.Locator("li").Nth(step);
        await Expect(card.Locator("a")).ToHaveAttributeAsync("href", expected[step].ContentUrl);
        await Expect(card.Locator("h3")).ToHaveTextAsync(expected[step].Title);
        await Expect(card.Locator("span")).ToHaveTextAsync($"Step {step + 1}");
      }
    }
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

  [Fact]
  public async Task AboutPage_ReusesCompactSupportCalloutAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "about/");
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "About I ❤️ .NET", Exact = true })).ToBeVisibleAsync();
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
          "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"));
    }
  }

  [Fact]
  public async Task AnalyticsPage_ShowsCompactSupportCalloutAfterContentAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "analytics/");
    var support = Page.GetByRole(AriaRole.Complementary, new() { Name = "Help more developers discover I ❤️ .NET" });
    await Expect(support).ToBeVisibleAsync();
    var headingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>("headings => headings.map(heading => heading.id)");
    Assert.Equal(new[] { "search-impressions-title", "search-click-milestones", "community-support-title" }, headingIds);
    await Expect(support.Locator("img")).ToHaveCountAsync(0);
    var star = support.GetByRole(AriaRole.Link, new() { Name = "Star on GitHub", Exact = false });
    var share = support.GetByRole(AriaRole.Link, new() { Name = "Share via WhatsApp", Exact = false });
    await Expect(star).ToHaveAttributeAsync("href", "https://github.com/ILoveDotNet/ilovedotnet");
    var shareUrl = await share.GetAttributeAsync("href");
    Assert.NotNull(shareUrl);
    Assert.StartsWith("https://api.whatsapp.com/send?text=", shareUrl);
    Assert.Contains("https://ilovedotnet.org/", Uri.UnescapeDataString(shareUrl));
    foreach (var link in new[] { star, share })
    {
      await Expect(link).ToHaveAttributeAsync("target", "_blank");
      await Expect(link).ToHaveAttributeAsync("rel", "noopener noreferrer");
      await link.FocusAsync();
      await Expect(link).ToBeFocusedAsync();
    }
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        Assert.True(await support.EvaluateAsync<bool>(
            "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"));
        foreach (var link in new[] { star, share })
        {
          var box = await link.BoundingBoxAsync();
          Assert.NotNull(box);
          Assert.True(box.Height >= 44);
        }
      }
    }
  }

  [Fact]
  public async Task AnalyticsPage_ShowsResponsiveHistoricalClickTimelineAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "analytics/");
    var timeline = Page.GetByRole(AriaRole.Region, new() { Name = "Historical Google Search click milestones" });
    await Expect(timeline).ToBeVisibleAsync();
    var expected = new SharedModels.Achievements().GoogleSearchImpacts.OrderByDescending(x => x.Date).ToArray();
    var cards = timeline.Locator("article");
    await Expect(cards).ToHaveCountAsync(expected.Length);
    for (var index = 0; index < expected.Length; index++)
    {
      await Expect(cards.Nth(index).Locator("h4")).ToHaveTextAsync($"{expected[index].Clicks:N0} clicks");
      await Expect(cards.Nth(index).Locator("time")).ToHaveAttributeAsync("datetime", expected[index].Date.ToString("yyyy-MM-dd"));
      if (index < 5)
      {
        await Expect(cards.Nth(index)).ToBeVisibleAsync();
      }
      else
      {
        await Expect(cards.Nth(index)).ToBeHiddenAsync();
      }
    }
    await Expect(timeline.GetByText("Latest recorded milestone", new() { Exact = true })).ToHaveCountAsync(1);
    var summary = timeline.Locator("summary");
    await Expect(summary).ToContainTextAsync($"{expected.Length - 5} earlier milestones");
    await summary.FocusAsync();
    await Page.Keyboard.PressAsync("Enter");
    await Expect(cards.Last).ToBeVisibleAsync();
    await Expect(timeline.Locator("h3")).ToHaveTextAsync(new[] { "2023", "2023", "2022" });
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        Assert.True(await timeline.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
            $"The timeline should fit at {width}px in {(dark ? "dark" : "light")} mode.");
      }
    }
    await summary.ClickAsync();
    await Expect(cards.Last).ToBeHiddenAsync();
    await Expect(Page.Locator("table caption")).ToContainTextAsync("Monthly Google Search impressions");
  }

  [Fact]
  public async Task AnalyticsPage_FiltersMonthlyImpressionsAndShowsExactDataAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "analytics/");
    var panel = Page.GetByRole(AriaRole.Region, new() { Name = "Google Search impressions", Exact = true });
    await Expect(panel).ToBeVisibleAsync();
    Assert.Equal("search-impressions-title", await Page.Locator("h2").First.GetAttributeAsync("id"));
    var chart = panel.GetByRole(AriaRole.Region, new() { Name = "Monthly impressions chart", Exact = true });
    var period = panel.GetByLabel("Reporting period");
    var records = new SharedModels.Achievements().GoogleSearchImpressions.OrderBy(x => x.Date).ToArray();
    await Expect(period).ToHaveValueAsync("recent");
    await Expect(chart.Locator("li")).ToHaveCountAsync(12);
    await Expect(chart.Locator("time").First).ToHaveAttributeAsync("datetime", records[^12].Date.ToString("yyyy-MM"));
    await Expect(chart.Locator("time").Last).ToHaveAttributeAsync("datetime", records[^1].Date.ToString("yyyy-MM"));
    await panel.Locator("summary").ClickAsync();
    await Expect(panel.Locator("tbody tr")).ToHaveCountAsync(12);
    foreach (var selection in new[] { "2023", "all", "recent" })
    {
      await period.SelectOptionAsync(selection);
      var expected = selection == "all" ? records : selection == "recent" ? records.TakeLast(12).ToArray() :
          records.Where(x => x.Date.Year == 2023).ToArray();
      await Expect(chart.Locator("li")).ToHaveCountAsync(expected.Length);
      await Expect(panel.Locator("tbody tr")).ToHaveCountAsync(expected.Length);
      for (var index = 0; index < expected.Length; index++)
      {
        await Expect(chart.Locator("time").Nth(index)).ToHaveAttributeAsync("datetime", expected[index].Date.ToString("yyyy-MM"));
        await Expect(panel.Locator("tbody tr").Nth(index).Locator("td")).ToHaveTextAsync(expected[index].Impressions.ToString("N0", System.Globalization.CultureInfo.InvariantCulture));
      }
      var maximum = expected.Max(x => x.Impressions);
      var heights = await chart.Locator("li div[style]").EvaluateAllAsync<double[]>(
          "bars => bars.map(bar => bar.getBoundingClientRect().height)");
      for (var index = 0; index < expected.Length; index++)
      {
        Assert.InRange(Math.Abs(heights[index] / heights.Max() - (double)expected[index].Impressions / maximum), 0, 0.01);
      }
    }
    foreach (var selection in new[] { "recent", "all" })
    {
      await period.SelectOptionAsync(selection);
      foreach (var width in new[] { 320, 375, 390, 414, 768, 1280 })
      {
        await Page.SetViewportSizeAsync(width, 844);
        foreach (var dark in new[] { false, true })
        {
          await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
          Assert.True(await panel.EvaluateAsync<bool>(
              "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
              $"The chart panel should fit at {width}px in {(dark ? "dark" : "light")} mode.");
          Assert.True(await Page.Locator("main").EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"),
              $"Chart contents must not widen the main page at {width}px.");
          Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
              $"The page must not scroll horizontally at {width}px.");
        }
      }
    }
    await chart.FocusAsync();
    await Page.Keyboard.PressAsync("ArrowRight");
    await Page.WaitForFunctionAsync("() => document.querySelector('[aria-label=\"Monthly impressions chart\"]').scrollLeft > 0");
    await Expect(panel.Locator("tbody tr").Last.Locator("td")).ToHaveTextAsync(records[^1].Impressions.ToString("N0", System.Globalization.CultureInfo.InvariantCulture));
  }

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
      "interview/",
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
  public async Task PrivacyPage_HasResponsiveContentsAndReadablePolicyLayoutAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "privacy/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Privacy Policy");
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
    Assert.Equal(0, await Page.Locator("article button").CountAsync());

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

  [Fact]
  public async Task PublicPages_HaveSinglePageSpecificH1Async()
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
      "analytics/",
      "interview/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var headings = Page.Locator("h1");
      await Expect(headings).ToHaveCountAsync(1);
      var text = (await headings.InnerTextAsync()).Trim();
      Assert.False(string.IsNullOrWhiteSpace(text), $"H1 on '{path}' should not be empty.");
      Assert.Equal(0, await Page.Locator("#brand h1, h1#brand").CountAsync());
    }
  }

  [Fact]
  public async Task InterviewPage_HasAnswerContentAndWorkingTopicLinksAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "interview/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("C# and .NET Interview Questions and Answers");

    foreach (var (sectionId, answerCount) in new[]
    {
      ("csharp-interview-questions", 6),
      ("dotnet-interview-questions", 6),
      ("system-design-interview", 3),
    })
    {
      var section = Page.Locator($"section[aria-labelledby='{sectionId}']");
      await Expect(section.Locator("details")).ToHaveCountAsync(answerCount);
      foreach (var answer in await section.Locator("details p").AllTextContentsAsync())
      {
        Assert.True(answer.Length >= 200, $"Expected a substantive answer in {sectionId}.");
      }
      var disclosure = section.Locator("details").First;
      await Expect(disclosure.Locator("p")).ToBeHiddenAsync();
      await disclosure.Locator("summary").FocusAsync();
      await Page.Keyboard.PressAsync("Enter");
      await Expect(disclosure.Locator("p")).ToBeVisibleAsync();
      await disclosure.Locator("summary").ClickAsync();
      await Expect(disclosure.Locator("p")).ToBeHiddenAsync();
    }

    var topicLinks = Page.Locator("nav[aria-label='Interview preparation topics'] a");
    await Expect(topicLinks).ToHaveCountAsync(6);
    for (var index = 0; index < await topicLinks.CountAsync(); index++)
    {
      var href = await topicLinks.Nth(index).GetAttributeAsync("href");
      Assert.NotNull(href);
      Assert.StartsWith("interview/#", href, StringComparison.Ordinal);
      var target = new Uri(WebBaseUrl, href);
      Assert.Equal("/interview/", target.AbsolutePath);
      await Expect(Page.Locator(target.Fragment)).ToHaveCountAsync(1);
    }

    await topicLinks.Nth(2).ClickAsync();
    Assert.Equal("/interview/", new Uri(Page.Url).AbsolutePath);
    Assert.Equal("#csharp-interview-questions", new Uri(Page.Url).Fragment);
    await Expect(Page.Locator("#csharp-interview-questions")).ToBeInViewportAsync();
    var headingBox = await Page.Locator("#csharp-interview-questions").BoundingBoxAsync();
    var headerBox = await Page.Locator("#header").BoundingBoxAsync();
    Assert.NotNull(headingBox);
    Assert.NotNull(headerBox);
    Assert.True(headingBox.Y >= headerBox.Y + headerBox.Height,
        "Topic headings should not be hidden beneath the sticky header.");

    await Page.SetViewportSizeAsync(390, 844);
    Assert.True(await Page.EvaluateAsync<bool>(
        "() => document.documentElement.scrollWidth <= window.innerWidth"),
        "Interview content should not introduce horizontal scrolling on mobile.");
  }

  [Fact]
  public async Task InterviewPage_HasCollectionAndBreadcrumbStructuredDataAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "interview/");
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
    var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      var collection = GetSchema(documents, "CollectionPage").RootElement;
      var canonical = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "link[rel='canonical']", "href");
      Assert.Equal(canonical, collection.GetProperty("@id").GetString());
      Assert.Equal(canonical, collection.GetProperty("url").GetString());
      Assert.Equal(await Page.TitleAsync(), collection.GetProperty("name").GetString());
      Assert.Equal("I ❤️ DotNet", collection.GetProperty("publisher").GetProperty("name").GetString());
      Assert.Equal("en", collection.GetProperty("inLanguage").GetString());
      Assert.True(collection.GetProperty("isAccessibleForFree").GetBoolean());

      var items = collection.GetProperty("mainEntity").GetProperty("itemListElement");
      Assert.Equal(6, items.GetArrayLength());
      var topicHrefs = await Page.Locator("nav[aria-label='Interview preparation topics'] a").EvaluateAllAsync<string[]>(
          "links => links.map(link => new URL(link.href).hash)");
      var headingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>(
          "headings => headings.map(heading => '#' + heading.id)");
      Assert.Equal(topicHrefs, headingIds);
      var position = 1;
      foreach (var item in items.EnumerateArray())
      {
        Assert.Equal(position++, item.GetProperty("position").GetInt32());
        var url = new Uri(item.GetProperty("url").GetString()!);
        Assert.Equal(topicHrefs[position - 2], url.Fragment);
        Assert.Equal(canonical, url.GetLeftPart(UriPartial.Path));
        await Expect(Page.Locator(url.Fragment)).ToHaveTextAsync(item.GetProperty("name").GetString()!);
      }

      AssertBreadcrumbSchema(GetSchema(documents, "BreadcrumbList"));
      await AssertSocialMetadataAsync(canonical, "website", "summary");
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
  public async Task CareerAndInterviewGuides_HaveActionFirstResponsiveLayoutsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "interview/");
    var readiness = Page.Locator("section[aria-labelledby='interview-readiness']");
    await Expect(readiness.Locator("article")).ToHaveCountAsync(14);
    await Expect(readiness.Locator("h3")).ToHaveCountAsync(3);
    await Expect(Page.Locator("section[aria-labelledby='preparation-plan'] li")).ToHaveCountAsync(7);
    Assert.Equal("interview-readiness", await Page.Locator("h2").First.GetAttributeAsync("id"));
    Assert.Equal("preparation-plan", await Page.Locator("h2").Nth(1).GetAttributeAsync("id"));
    var faq = Page.Locator("section[aria-labelledby='interview-faq'] details");
    await Expect(faq).ToHaveCountAsync(3);
    await faq.First.Locator("summary").ClickAsync();
    await Expect(faq.First.Locator("p")).ToBeVisibleAsync();

    foreach (var path in new[] { "interview/", "career/" })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await Expect(Page.Locator("main h1")).ToBeVisibleAsync();
      foreach (var width in new[] { 320, 390, 768, 1280 })
      {
        await Page.SetViewportSizeAsync(width, 844);
        foreach (var darkMode in new[] { false, true })
        {
          await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", darkMode);
          Assert.True(await Page.EvaluateAsync<bool>(
              "() => { const main = document.querySelector('main'); return main.scrollWidth <= main.clientWidth && main.getBoundingClientRect().right <= innerWidth; }"),
              $"{path} should not overflow at {width}px in {(darkMode ? "dark" : "light")} mode.");
          var cards = Page.Locator(path == "interview/" ?
              "section[aria-labelledby='delivery-readiness-title'] article" :
              "section[aria-labelledby='career-growth-title'] article");
          var first = await cards.Nth(0).BoundingBoxAsync();
          var second = await cards.Nth(1).BoundingBoxAsync();
          Assert.NotNull(first);
          Assert.NotNull(second);
          if (width < 768)
          {
            Assert.True(second.Y > first.Y, "Substantial cards should stack on mobile.");
          }
          else
          {
            Assert.Equal(first.Y, second.Y);
            Assert.True(second.X > first.X, "Substantial cards should use two columns on larger screens.");
          }
        }
      }
    }

    var careerHeadingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>(
        "headings => headings.map(heading => heading.id)");
    Assert.Equal(new[] { "career-action-title", "career-foundations-title", "career-growth-title", "career-interview-title" }, careerHeadingIds);
    await Expect(Page.Locator("section[aria-labelledby='career-growth-title'] article")).ToHaveCountAsync(7);
    await Expect(Page.GetByRole(AriaRole.Complementary, new() { Name = "Conversation starter" })).ToBeVisibleAsync();
    foreach (var link in await Page.Locator("nav[aria-label='Career guide topics'] a").AllAsync())
    {
      await link.ClickAsync();
      var target = new Uri(Page.Url).Fragment;
      await Expect(Page.Locator(target)).ToBeInViewportAsync();
    }
    await Page.GetByRole(AriaRole.Link, new() { Name = "Explore the .NET interview preparation guide" }).ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("C# and .NET Interview Questions and Answers");
  }

  [Fact]
  public async Task InterviewPage_IsLinkedFromHomeAndLearningPathAsync()
  {
    foreach (var path in new[] { "", "learningpath/" })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      var link = Page.GetByRole(AriaRole.Link, new()
      {
        Name = "Start preparing for your C# and .NET interview",
        Exact = true,
      });
      await Expect(link).ToHaveCountAsync(1);
      await Expect(link).ToHaveAttributeAsync("href", "interview/");
      await Expect(link).ToHaveTextAsync("Start preparing");
      await link.ClickAsync();
      await Expect(Page.Locator("h1")).ToHaveTextAsync("C# and .NET Interview Questions and Answers");
      await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
    }
  }

  [Fact]
  public async Task HomePage_ShowsPublishedLearningStatsAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var stats = Page.GetByRole(AriaRole.Region, new() { Name = "Learning at I Love .NET" });
    var publishedContents = new SharedModels.TableOfContents().Contents;
    var values = stats.Locator("dd");
    await Expect(values).ToHaveTextAsync(new[]
    {
      publishedContents.Count.ToString(),
      publishedContents.Select(content => content.Channel).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(),
      "6+",
      "10+",
    });
    await Expect(stats).ToContainTextAsync("100% free to learn");
    await Expect(stats).ToContainTextAsync("A new article every Sunday");

    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      var columns = await stats.Locator("dl").EvaluateAsync<int>(
          "element => getComputedStyle(element).gridTemplateColumns.split(' ').length");
      Assert.Equal(width >= 1024 ? 4 : 2, columns);
      Assert.True(await stats.Locator("dl").EvaluateAsync<bool>(
          "element => element.scrollWidth <= element.clientWidth"));
    }
  }

  [Fact]
  public async Task HomePage_ChannelShelfSupportsScrollingAndNavigationAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var shelf = Page.GetByRole(AriaRole.List, new() { Name = "Browse topic channels" });
    var metadata = new SharedModels.TableOfContents();
    var channels = metadata.AvailableContentTypes;
    var cards = shelf.GetByRole(AriaRole.Link);
    await Expect(cards).ToHaveCountAsync(channels.Count);
    for (var index = 0; index < channels.Count; index++)
    {
      var channel = channels[index];
      var count = metadata.Contents.Count(content => content.Channel == channel);
      await Expect(cards.Nth(index)).ToContainTextAsync(channel);
      await Expect(cards.Nth(index)).ToContainTextAsync($"{count} {(count == 1 ? "article" : "articles")}");
    }

    await Page.EmulateMediaAsync(new() { ReducedMotion = ReducedMotion.Reduce });
    await Page.GetByRole(AriaRole.Button, new() { Name = "Next channels", Exact = true }).ClickAsync();
    Assert.True(await shelf.EvaluateAsync<bool>("element => element.scrollLeft > 0"));
    await Page.GetByRole(AriaRole.Button, new() { Name = "Previous channels", Exact = true }).ClickAsync();
    await Expect(shelf).ToHaveJSPropertyAsync("scrollLeft", 0);

    await Page.SetViewportSizeAsync(320, 900);
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Next channels", Exact = true })).ToBeHiddenAsync();
    Assert.True(await shelf.EvaluateAsync<bool>("element => element.scrollWidth > element.clientWidth"));
    Assert.True(await cards.First.EvaluateAsync<bool>("element => element.getBoundingClientRect().height < 200"));
    await cards.Last.FocusAsync();
    Assert.True(await shelf.EvaluateAsync<bool>("element => element.scrollLeft > 0"));
    await Expect(cards.Last).ToBeInViewportAsync();
    await cards.First.ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync(channels[0], new() { IgnoreCase = true });
  }

  [Fact]
  public async Task AuthorPage_ShowsProfileAndPublishedArticlesAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "authors/abdul-rahman/");
    var metadata = new SharedModels.TableOfContents();
    var author = metadata.Authors.Single(item => item.Name == "Abdul Rahman");
    var articles = metadata.Contents.Where(content => content.Author == author.Name).ToList();
    await Expect(Page.Locator("h1")).ToHaveTextAsync(author.Name);
    await Expect(Page.Locator("dl[aria-label='Author contributions'] dd")).ToHaveTextAsync(new[]
    {
      articles.Count.ToString(),
      articles.Select(content => content.Channel).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(),
      $"{author.IndustryExperienceYears}+",
    });
    var socialProfiles = Page.GetByRole(AriaRole.Navigation, new() { Name = "Author social profiles" });
    await Expect(socialProfiles.GetByRole(AriaRole.Link)).ToHaveCountAsync(author.SocialUrls.Count);
    await Expect(socialProfiles.Locator("a svg")).ToHaveCountAsync(author.SocialUrls.Count);
    await Expect(socialProfiles.GetByRole(AriaRole.Link, new() { Name = "LinkedIn", Exact = true })).ToBeVisibleAsync();
    var latest = Page.GetByRole(AriaRole.Region, new() { Name = $"Latest articles by {author.Name}" });
    foreach (var article in articles.OrderByDescending(content => content.ModifiedOn).ThenByDescending(content => content.CreatedOn).Take(6))
    {
      await Expect(latest.Locator($"a[href='/{article.ContentUrl.Trim('/')}/']")).ToHaveCountAsync(2);
    }
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      Assert.True(await socialProfiles.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
      Assert.True(await latest.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
    }
  }

  [Fact]
  public async Task HomePage_AuthorCardLinksToProfileAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var authors = Page.GetByRole(AriaRole.List, new() { Name = "Meet the author", Exact = true });
    var card = authors.GetByRole(AriaRole.Link);
    await Expect(card).ToHaveCountAsync(1);
    await Expect(card).ToContainTextAsync("Abdul Rahman");
    await Expect(card).ToContainTextAsync("Microsoft MVP");
    await Expect(card).ToContainTextAsync("View author profile");
    await Expect(card).ToHaveAttributeAsync("href", "/authors/abdul-rahman/");
    await authors.ScrollIntoViewIfNeededAsync();
    await Expect(card.Locator("img")).ToHaveJSPropertyAsync("complete", true);
    Assert.True(await card.Locator("img").EvaluateAsync<bool>("element => element.naturalWidth > 0"));
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      Assert.True(await card.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
      var direction = await card.EvaluateAsync<string>("element => getComputedStyle(element).flexDirection");
      Assert.Equal(width < 640 ? "column" : "row", direction);
    }
    await card.ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Abdul Rahman");
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
      "interview/",
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
      ("learningpath/", ".NET Tutorial Learning Paths"),
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
