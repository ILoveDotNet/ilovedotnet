using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class HomeTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task Navigation_LearningChatIsVisibleOnMobileAndDesktopAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var navigation = Page.Locator("nav").Filter(new() { Has = Page.Locator("a[href='/chat/']") });
    var chat = navigation.GetByRole(AriaRole.Link, new() { Name = "Learn with AI", Exact = true });
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      await Expect(chat).ToBeVisibleAsync();
      await Expect(chat.Locator("svg")).ToBeVisibleAsync();
      await Expect(chat.Locator("span:visible")).ToHaveTextAsync(width < 768 ? "Chat" : "Learn with AI");
      foreach (var link in await navigation.GetByRole(AriaRole.Link).AllAsync())
      {
        await Expect(link).ToBeVisibleAsync();
        var target = width < 768 ? link : link.Locator("svg");
        Assert.True(await target.EvaluateAsync<bool>(
            "element => { const bounds = element.getBoundingClientRect(); return bounds.left >= 0 && bounds.right <= innerWidth; }"),
            $"Navigation item '{await link.InnerTextAsync()}' extends outside the {width}px viewport.");
      }
      Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
    }

    await Page.SetViewportSizeAsync(390, 900);
    await chat.ClickAsync();
    await Expect(Page).ToHaveURLAsync(new Uri(WebBaseUrl, "chat/").ToString());
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Learn with AI", Exact = true })).ToBeVisibleAsync();
    await Expect(chat).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bactive\b"));
  }

  [Fact]
  public async Task HomePage_HasMetadataPublishedStatsAndResponsiveThumbnailsAsync()
  {
    var response = await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    Assert.Equal(WebBaseUrl.AbsoluteUri, response.Url);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Learn .NET by building.");
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Channels", Exact = true })).ToBeVisibleAsync();
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, "");
    foreach (var content in await Page.Locator("meta[property='robots'], meta[name='robots']")
        .EvaluateAllAsync<string[]>("elements => elements.map(element => element.content)"))
    {
      Assert.DoesNotContain("noindex", content, StringComparison.OrdinalIgnoreCase);
    }
    var stats = Page.GetByRole(AriaRole.Region, new() { Name = "Learning at I Love .NET" });
    var publishedContents = new SharedModels.TableOfContents().Contents;
    await Expect(stats.Locator("dd")).ToHaveTextAsync(new[]
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
    await SeoTestHelpers.AssertResponsiveThumbnailCardAsync(Page, clamped: true);
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
}
