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
      await Expect(card.Locator(":scope > article")).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("md:hover:scale-110"));
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
  public async Task PublicPages_HaveCanonicalMetadataAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
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
  public async Task PublicPages_HaveSinglePageSpecificH1Async()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
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
  public async Task HomePage_FiltersAndPaginatesAccessibleTutorialFeedAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var table = new SharedModels.TableOfContents();
    var feed = Page.Locator("section[aria-labelledby='latest-tutorials-title']");
    var filters = Page.GetByRole(AriaRole.Group, new() { Name = "Filter tutorials by topic" });
    await Expect(feed.Locator("[data-thumbnail]")).ToHaveCountAsync(12);
    await Expect(feed.Locator("h2")).ToHaveTextAsync("Latest tutorials");
    await feed.GetByRole(AriaRole.Button, new() { Name = "Show more tutorials" }).ClickAsync();
    await Expect(feed.Locator("[data-thumbnail]")).ToHaveCountAsync(24);
    await filters.GetByRole(AriaRole.Button, new() { Name = "LINQ", Exact = true }).FocusAsync();
    await Page.Keyboard.PressAsync("Enter");
    await Expect(filters.GetByRole(AriaRole.Button, new() { Name = "LINQ", Exact = true })).ToHaveAttributeAsync("aria-pressed", "true");
    await Expect(feed.Locator("[data-thumbnail]")).ToHaveCountAsync(Math.Min(12, table.SelectedContentTypeTotalCount("LINQ")));
    var expected = table.FilteredAndPagedContents("LINQ", 0, int.MaxValue);
    await Expect(feed.Locator("h2")).ToHaveTextAsync("Latest LINQ tutorials");
    while (await feed.GetByRole(AriaRole.Button, new() { Name = "Show more tutorials" }).CountAsync() > 0)
    {
      await feed.GetByRole(AriaRole.Button, new() { Name = "Show more tutorials" }).ClickAsync();
    }
    await Expect(feed.Locator("[data-thumbnail] h3")).ToHaveTextAsync(expected.Select(content => content.Title).ToArray());
    await Expect(feed.GetByRole(AriaRole.Status)).ToHaveTextAsync($"Showing {expected.Count} of {expected.Count} tutorials");
    await filters.GetByRole(AriaRole.Button, new() { Name = "All", Exact = true }).ClickAsync();
    await Expect(feed.Locator("[data-thumbnail]")).ToHaveCountAsync(12);
    await Expect(filters.GetByRole(AriaRole.Button, new() { Name = "All", Exact = true })).ToHaveAttributeAsync("aria-pressed", "true");
    foreach (var dark in new[] { false, true })
    {
      await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
      foreach (var width in new[] { 320, 375, 768, 1024, 1440, 1600 })
      {
        await Page.SetViewportSizeAsync(width, 900);
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
        Assert.True(await filters.EvaluateAsync<bool>("element => element.getBoundingClientRect().right <= innerWidth"));
        var columns = await feed.Locator("div.grid").EvaluateAsync<int>("element => getComputedStyle(element).gridTemplateColumns.split(' ').length");
        Assert.Equal(width >= 1536 ? 5 : width >= 1280 ? 4 : width >= 1024 ? 3 : width >= 768 ? 2 : 1, columns);
      }
    }
    foreach (var channel in table.AvailableContentTypes.Where(channel => table.SelectedContentTypeTotalCount(channel) == 0))
    {
      await filters.GetByRole(AriaRole.Button, new() { Name = channel, Exact = true }).ClickAsync();
      await Expect(feed.Locator("[data-thumbnail]")).ToHaveCountAsync(0);
      await Expect(feed).ToContainTextAsync("No published tutorials in this topic yet.");
      await Expect(feed.GetByRole(AriaRole.Button, new() { Name = "Show more tutorials" })).ToHaveCountAsync(0);
    }
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Announcements", Exact = true })).ToHaveCountAsync(0);
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
      await Expect(cards.Nth(index)).ToContainTextAsync(count == 0 ? "Coming soon" : $"{count} {(count == 1 ? "article" : "articles")}");
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
  public async Task ThumbnailCards_KeepPostersReadableAndFitAllSharedSurfacesAsync()
  {
    foreach (var (path, clamped) in new[]
    {
      ("", true),
      ("blogs/dependency-injection-lifetimes-in-dotnet/", false),
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await SeoTestHelpers.AssertResponsiveThumbnailCardAsync(Page, clamped);
    }
  }

  [Fact]
  public async Task IndexablePages_DoNotDeclareNoIndexAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
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
    await AssertSocialMetadataAsync(Page, canonical, "article", "summary_large_image");
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

      var author = article.RootElement.GetProperty("author");
      var authorUrl = SeoTestHelpers.ExpectedUrl(ExpectedCanonicalBaseUrl, "authors/abdul-rahman/");
      Assert.Equal("Person", author.GetProperty("@type").GetString());
      Assert.Equal("Abdul Rahman", author.GetProperty("name").GetString());
      Assert.Equal(authorUrl, author.GetProperty("url").GetString());
      Assert.Equal($"{authorUrl}#person", author.GetProperty("@id").GetString());

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
  public async Task PublicWebsitePages_HaveValidWebsiteStructuredDataAsync()
  {
    foreach (var path in new[] { "", "chat/" })
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
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await SeoTestHelpers.AssertImageAttributesAsync(Page, path);
    }
  }

  [Fact]
  public async Task RepresentativePages_UseCanonicalInternalLinkFormatsAsync()
  {
    foreach (var path in new[]
    {
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
    })
    {
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
      await SeoTestHelpers.AssertCanonicalInternalLinksAsync(Page, WebBaseUrl, path);
    }
  }

  [Fact]
  public async Task CanonicalRoutes_Return200WithoutRedirectsAsync()
  {
    foreach (var path in new[]
    {
      "",
      "blogs/using-github-copilot-ai-for-navigating-new-codebase/",
    })
    {
      var expectedUrl = SeoTestHelpers.ExpectedUrl(WebBaseUrl, path);
      var response = await Page.GotoAsync(expectedUrl);
      Assert.NotNull(response);
      Assert.Equal(200, response.Status);
      Assert.Equal(expectedUrl, response.Url);
    }
  }

}
