using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class HomeTests(AppFixture fixture) : DistributedPageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Theory]
  [InlineData("")]
  [InlineData("blogs/dependency-injection-lifetimes-in-dotnet/?source=keyboard#why")]
  public async Task ReaderShell_SkipLinkFocusesCurrentPageContentAsync(string path)
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    var originalUrl = Page.Url.Split('#')[0];
    var skip = Page.GetByRole(AriaRole.Link, new() { Name = "Skip to content", Exact = true });
    await Page.Locator("#header :is(a, button, input):visible").First.FocusAsync();
    await Page.Keyboard.PressAsync("Shift+Tab");
    await Expect(skip).ToBeFocusedAsync();
    Assert.True(await skip.EvaluateAsync<bool>("element => element.getBoundingClientRect().top >= 0"));
    await Page.Keyboard.PressAsync("Enter");
    await Expect(Page).ToHaveURLAsync($"{originalUrl}#main");
    await Expect(Page.Locator("#main")).ToBeFocusedAsync();
  }

  [Fact]
  public async Task ReaderShell_ReflowsSearchAndNavigationWithoutReloadAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var input = Page.GetByRole(AriaRole.Combobox, new() { Name = "Search articles" });
    await Expect(input).ToBeVisibleAsync();
    var viewport = await Page.Locator("meta[name='viewport']").GetAttributeAsync("content");
    Assert.DoesNotContain("user-scalable", viewport);
    Assert.DoesNotContain("maximum-scale", viewport);
    await Page.SetViewportSizeAsync(320, 900);
    await Expect(input).ToBeHiddenAsync();
    await Page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true }).ClickAsync();
    await Expect(input).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand")).ToBeHiddenAsync();
    await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Join our WhatsApp channel" })).ToBeHiddenAsync();
    await Page.SetViewportSizeAsync(1280, 720);
    await Expect(input).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand")).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Exit Search" })).ToBeHiddenAsync();
    await Page.SetViewportSizeAsync(320, 900);
    await Expect(input).ToBeHiddenAsync();
    foreach (var control in await Page.Locator("#header :is(a, button):visible").AllAsync())
    {
      Assert.True(await control.EvaluateAsync<bool>(
          "element => { const bounds = element.getBoundingClientRect(); return bounds.width >= 44 && bounds.height >= 44; }"));
    }
    var navigation = Page.GetByRole(AriaRole.Navigation, new() { Name = "Primary", Exact = true });
    Assert.True(await navigation.EvaluateAsync<bool>(
        "element => { const bounds = element.getBoundingClientRect(); return Math.abs(bounds.bottom - innerHeight) < 1 && parseFloat(getComputedStyle(document.querySelector('#main')).paddingBottom) >= bounds.height; }"));
    foreach (var link in await navigation.GetByRole(AriaRole.Link).AllAsync())
    {
      Assert.True(await link.EvaluateAsync<bool>(
          "element => { const bounds = element.getBoundingClientRect(); return bounds.width >= 44 && bounds.height >= 44; }"));
    }
    Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"));
    await Page.SetViewportSizeAsync(1280, 240);
    var lastLink = navigation.GetByRole(AriaRole.Link).Last;
    await lastLink.FocusAsync();
    Assert.True(await lastLink.EvaluateAsync<bool>(
        "element => { const bounds = element.getBoundingClientRect(); return bounds.top >= document.querySelector('#header').getBoundingClientRect().bottom && bounds.bottom <= innerHeight; }"));
  }

  [Fact]
  public async Task MobileTheme_LeavesSupportLinksVisibleAndPersistsAcrossLayoutsAsync()
  {
    await Page.SetViewportSizeAsync(320, 700);
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Theme Selector" })).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Join our WhatsApp channel" })).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Give a Star in GitHub" })).ToBeVisibleAsync();
    var controls = await Page.Locator("#header :is(a, button):visible").AllAsync();
    for (var index = 1; index < controls.Count; index++)
    {
      var previous = await controls[index - 1].BoundingBoxAsync();
      var current = await controls[index].BoundingBoxAsync();
      Assert.NotNull(previous);
      Assert.NotNull(current);
      Assert.True(current.X >= previous.X + previous.Width - 1, "Header touch targets must not overlap.");
    }

    await Page.GetByRole(AriaRole.Button, new() { Name = "Others", Exact = true }).ClickAsync();
    var options = Page.GetByRole(AriaRole.Region, new() { Name = "More options" });
    var theme = options.GetByRole(AriaRole.Group, new() { Name = "Theme", Exact = true });
    await theme.GetByRole(AriaRole.Radio, new() { Name = "Light", Exact = true }).CheckAsync();
    await Expect(Page.Locator("html")).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bdark\b"));
    await theme.GetByRole(AriaRole.Radio, new() { Name = "Light", Exact = true }).PressAsync("ArrowDown");
    await Expect(theme.GetByRole(AriaRole.Radio, new() { Name = "Dark", Exact = true })).ToBeCheckedAsync();
    await Expect(options).ToBeVisibleAsync();
    await Expect(Page.Locator("html")).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bdark\b"));
    await Page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true }).FocusAsync();
    await Expect(options).ToBeHiddenAsync();

    await Page.SetViewportSizeAsync(1280, 800);
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Theme Selector" })).ToBeVisibleAsync();
    await Page.SetViewportSizeAsync(390, 700);
    await Page.ReloadAsync();
    await Page.GetByRole(AriaRole.Button, new() { Name = "Others", Exact = true }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Radio, new() { Name = "Dark", Exact = true })).ToBeCheckedAsync();
  }

  [Fact]
  public async Task ReaderShell_RespectsReducedMotionAndAnnouncesNavigationStateAsync()
  {
    await Page.EmulateMediaAsync(new() { ReducedMotion = ReducedMotion.Reduce });
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    foreach (var pulse in await Page.Locator("#header .animate-pulse").AllAsync())
    {
      await Expect(pulse).ToHaveCSSAsync("animation-name", "none");
    }

    var toggle = Page.GetByRole(AriaRole.Button, new() { Name = "Toggle Menu", Exact = true });
    var navigation = Page.GetByRole(AriaRole.Navigation, new() { Name = "Primary", Exact = true });
    await Expect(toggle).ToHaveAttributeAsync("aria-expanded", "true");
    await toggle.ClickAsync();
    await Expect(navigation).ToBeHiddenAsync();
    await Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
    await toggle.ClickAsync();
    await Expect(navigation).ToBeVisibleAsync();
    await Expect(toggle).ToHaveAttributeAsync("aria-expanded", "true");
    await Expect(Page.Locator("#main")).ToHaveCSSAsync("transition-duration", "0s");
  }

  [Fact]
  public async Task Navigation_LearningChatIsVisibleOnMobileAndDesktopAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var navigation = Page.Locator("nav").Filter(new() { Has = Page.Locator("a[href='/chat/']") });
    var chat = navigation.GetByRole(AriaRole.Link, new() { Name = "Learn with AI Chat", Exact = true });
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      await Expect(chat).ToBeVisibleAsync();
      await Expect(chat.Locator("svg")).ToBeVisibleAsync();
      await Expect(chat.Locator("span:visible")).ToHaveTextAsync(width < 768 ? "Chat" : "Learn with AI");
      await Expect(chat).ToHaveAccessibleNameAsync("Learn with AI Chat");
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
  public async Task Navigation_SecondaryPagesAreAvailableInOthersMenuAsync()
  {
    foreach (var width in new[] { 320, 390, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
      var navigation = Page.Locator("nav").Filter(new() { Has = Page.Locator("a[href='/chat/']") });
      await Expect(navigation.GetByRole(AriaRole.Link)).ToHaveCountAsync(5);
      await Expect(navigation.Locator("a[href='/about'], a[href='/analytics']")).ToHaveCountAsync(0);
      var menu = Page.Locator("#others-menu");
      foreach (var name in new[] { "About", "Analytics" })
      {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Others", Exact = true }).ClickAsync();
        await Expect(menu).ToBeVisibleAsync();
        foreach (var item in new[] { "About", "Analytics", "Privacy", "Disclaimer" })
        {
          await Expect(menu.GetByRole(AriaRole.Link, new() { Name = item, Exact = true })).ToBeVisibleAsync();
        }
        Assert.True(await menu.EvaluateAsync<bool>(
            "element => { const bounds = element.getBoundingClientRect(); return bounds.left >= 0 && bounds.right <= innerWidth; }"));
        await menu.GetByRole(AriaRole.Link, new() { Name = name, Exact = true }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Uri(WebBaseUrl, name.ToLowerInvariant()).ToString());
        await Expect(Page.Locator("h1")).ToHaveTextAsync(name == "About" ? "About I ❤️ .NET" : "Analytics");
        await Expect(menu).ToBeHiddenAsync();
      }
    }
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
