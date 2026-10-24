using ILoveDotNet.DistributedTest.Utils;
using static ILoveDotNet.DistributedTest.Utils.SeoTestHelpers;

namespace ILoveDotNet.DistributedTest;

public class BlogTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");
  private readonly Uri ExpectedCanonicalBaseUrl = GetExpectedCanonicalBaseUrl();

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task BlogPage_HasMetadataStructuredDataAndAccessibleContentAsync()
  {
    const string path = "blogs/using-github-copilot-ai-for-navigating-new-codebase/";
    var response = await GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    Assert.Equal(ExpectedUrl(WebBaseUrl, path), response.Url);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Using GitHub Copilot AI for Navigating New Codebase");
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    var canonical = await AssertCanonicalMetadataAsync(Page, path);
    await AssertSocialMetadataAsync(Page, canonical, "article", "summary_large_image");
    foreach (var content in await Page.Locator("meta[property='robots'], meta[name='robots']")
        .EvaluateAllAsync<string[]>("elements => elements.map(element => element.content)"))
    {
      Assert.DoesNotContain("noindex", content, StringComparison.OrdinalIgnoreCase);
    }
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
    var documents = await GetJsonLdDocumentsAsync(Page);
    try
    {
      var article = GetSchema(documents, "Article");
      foreach (var property in new[] { "headline", "description", "author", "datePublished", "dateModified", "image", "mainEntityOfPage" })
      {
        Assert.True(article.RootElement.TryGetProperty(property, out _), $"Article schema must contain '{property}'.");
      }
      Assert.Equal(canonical, article.RootElement.GetProperty("mainEntityOfPage").GetProperty("@id").GetString());
      var author = article.RootElement.GetProperty("author");
      var authorUrl = ExpectedUrl(ExpectedCanonicalBaseUrl, "authors/abdul-rahman/");
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
    var breadcrumb = Page.Locator("nav[aria-label='Breadcrumb']");
    await Expect(breadcrumb).ToHaveCountAsync(1);
    await Expect(breadcrumb.Locator("a")).ToHaveTextAsync(["Home", "AI"]);
    await Expect(breadcrumb.Locator("li[aria-current='page']"))
        .ToContainTextAsync("Using GitHub Copilot AI for Navigating New Codebase");
    await AssertImageAttributesAsync(Page, path);
    await AssertCanonicalInternalLinksAsync(Page, WebBaseUrl, path);
    await Page.SetViewportSizeAsync(320, 900);
    Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
        $"Article {path} must fit at 320px.");
  }

  [Fact]
  public async Task BlogPage_FitsResponsiveReadingCodeRecommendationsAndBoundedAdsAsync()
  {
    await GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
    await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
    var sidebar = Page.Locator("aside[aria-label='Article resources']");
    var sidebarAd = sidebar.GetByRole(AriaRole.Region, new() { Name = "Advertisement", Exact = true });
    var stickyAd = Page.GetByRole(AriaRole.Region, new() { Name = "Advertisement", Exact = true });
    await Expect(sidebarAd).ToHaveCountAsync(1);
    await Expect(sidebarAd).ToContainTextAsync("Ad Space");
    await Expect(sidebar.Locator("section[aria-labelledby='related-articles'] > section[aria-label='Advertisement']")).ToHaveCountAsync(1);
    await Expect(sidebar.Locator(":scope > section")).ToHaveAttributeAsync("aria-labelledby", "related-articles");
    var card = Page.Locator("section[aria-labelledby='related-articles'] > div[aria-live] > div").First;
    await Expect(card.Locator(":scope > article")).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("md:hover:scale-110"));
    await Expect(card.Locator("img")).ToHaveCountAsync(2);
    await Expect(card.Locator("time")).ToHaveAttributeAsync("datetime", new System.Text.RegularExpressions.Regex(@"^\d{4}-\d{2}-\d{2}$"));
    foreach (var width in new[] { 320, 375, 390, 414, 768, 1024, 1280, 1440 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        await Page.EvaluateAsync("() => { document.documentElement.style.scrollBehavior = 'auto'; window.scrollTo(0, 0); }");
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Article page must not scroll horizontally at {width}px in {(dark ? "dark" : "light")} mode.");
        Assert.True(await sidebar.EvaluateAsync<bool>(
            "element => { const style = getComputedStyle(element); return style.position === 'static' && style.overflowY === 'visible' && element.scrollHeight <= element.clientHeight + 1; }"));
        Assert.True(await sidebarAd.EvaluateAsync<bool>(
            "element => element.getBoundingClientRect().bottom <= document.querySelector('#related-articles').getBoundingClientRect().top"));
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
          await Expect(Page.Locator("details")).Not.ToHaveAttributeAsync("open", "");
          await Page.Locator("details summary").ClickAsync();
          await Page.GetByRole(AriaRole.Link, new() { Name = "Implementation", Exact = true }).ClickAsync();
          await Expect(Page.Locator("#how")).ToBeInViewportAsync();
          await Page.Locator("details summary").ClickAsync();
        }
        await Expect(stickyAd).ToHaveCSSAsync("position", width >= 1024 ? "sticky" : "static");
        Assert.True(await card.EvaluateAsync<bool>("element => { const poster = element.querySelector('header img').getBoundingClientRect(); const title = element.querySelector('h3'); const bounds = title.getBoundingClientRect(); return Math.abs(poster.width - element.getBoundingClientRect().width) < 1 && bounds.top >= poster.bottom && getComputedStyle(title).webkitLineClamp === 'none'; }"));
        if (width >= 1024)
        {
          await stickyAd.EvaluateAsync("element => window.scrollTo(0, element.getBoundingClientRect().top + scrollY + 100)");
          await Page.WaitForFunctionAsync("() => Math.abs(document.querySelector('section[aria-label=\"Advertisement\"]').getBoundingClientRect().top - 56) < 1");
          await Page.EvaluateAsync("() => window.scrollTo(0, document.documentElement.scrollHeight)");
          Assert.True(await stickyAd.EvaluateAsync<bool>(
              "element => { const ad = element.getBoundingClientRect(); const section = element.parentElement.getBoundingClientRect(); return ad.bottom <= section.bottom + 1; }"));
        }
      }
    }
    await Expect(Page.Locator("section[aria-label='Article body']")).ToContainTextAsync("Ad Space");
    await Page.SetViewportSizeAsync(320, 900);
    var code = Page.GetByRole(AriaRole.Region, new() { Name = "Razor code", Exact = true }).First;
    await code.FocusAsync();
    await Page.Keyboard.PressAsync("ArrowRight");
    await Page.WaitForFunctionAsync("() => document.querySelector('pre').scrollLeft > 0");
    Assert.True(await code.EvaluateAsync<bool>("element => element.scrollWidth > element.clientWidth"));
    await AssertResponsiveThumbnailCardAsync(Page, clamped: false);
    await Expect(Page.Locator("[data-sidebar-ad-track]")).ToHaveCountAsync(0);
    await Page.EvaluateAsync("() => Blazor.navigateTo('/learningpath/')");
    await Expect(Page.Locator("section[aria-labelledby='related-articles']")).ToHaveCountAsync(0);
    await Page.SetViewportSizeAsync(320, 900);
    var webApiPath = new SharedModels.TableOfContents().Contents.First(content => content.Channel == "Web API").ContentUrl;
    await GotoSuccessfulPageAsync(Page, WebBaseUrl, webApiPath);
    await Expect(Page.Locator("#article-title")).ToBeVisibleAsync();
    Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
        $"Article {webApiPath} must fit at 320px.");
  }

  [Fact]
  public async Task BlogPage_ShowsReaderFirstHeaderAndWorkingAnchorsAsync()
  {
    const string slug = "dependency-injection-lifetimes-in-dotnet";
    var contents = new SharedModels.TableOfContents().Contents;
    var metadata = contents.Single(content => content.Slug == slug);
    await GotoSuccessfulPageAsync(Page, WebBaseUrl, $"blogs/{slug}/?source=test");
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
  public async Task BlogPage_PaginatesOnlyRelatedPublishedArticlesAndHandlesSingleArticleAsync()
  {
    var table = new SharedModels.TableOfContents();
    var metadata = table.Contents.First(content => content.Channel == "LINQ");
    var expected = table.GetRelatedContents(metadata.Slug, int.MaxValue);
    await GotoSuccessfulPageAsync(Page, WebBaseUrl, metadata.ContentUrl);
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
  public async Task BlogPage_CopiesCodeAndReportsClipboardFailureAsync()
  {
    await Context.GrantPermissionsAsync(["clipboard-read", "clipboard-write"]);
    await GotoSuccessfulPageAsync(Page, WebBaseUrl, "blogs/dependency-injection-lifetimes-in-dotnet/");
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
}
