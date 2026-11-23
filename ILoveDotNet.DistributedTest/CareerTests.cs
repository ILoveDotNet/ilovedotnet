using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class CareerTests(AppFixture fixture) : DistributedPageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task CareerPage_HasMetadataAndResponsiveGuideWithWorkingNavigationAsync()
  {
    const string path = "career/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Developer career growth");
    await Expect(Page.Locator("main h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);
    var startingPoints = Page.GetByRole(AriaRole.Navigation, new() { Name = "Career growth starting points" });
    await Expect(startingPoints.GetByRole(AriaRole.Link)).ToHaveCountAsync(3);
    await Expect(startingPoints.GetByText("Explore these connected practices in any order.", new() { Exact = true })).ToBeVisibleAsync();
    await Expect(startingPoints.GetByRole(AriaRole.Link, new() { Name = "Build your skills" }))
        .ToHaveAttributeAsync("href", "career/#career-foundations-title");
    await Expect(startingPoints.GetByRole(AriaRole.Link, new() { Name = "Show your impact" }))
        .ToHaveAttributeAsync("href", "career/#career-growth-title");
    await Expect(startingPoints.GetByRole(AriaRole.Link, new() { Name = "Take on a new challenge" }))
        .ToHaveAttributeAsync("href", "career/#career-conversation-starter");

    var headingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>(
        "headings => headings.map(heading => heading.id)");
    Assert.Equal(new[] { "career-action-title", "career-foundations-title", "career-growth-title", "career-interview-title" }, headingIds);
    var cards = Page.Locator("section[aria-labelledby='career-growth-title'] article");
    await Expect(cards).ToHaveCountAsync(7);
    var guidance = cards.Locator("details");
    await Expect(guidance).ToHaveCountAsync(7);
    await Expect(Page.Locator("section[aria-labelledby='career-foundations-title'] details")).ToHaveCountAsync(3);
    foreach (var disclosure in await Page.Locator("details.guide-disclosure").AllAsync())
    {
      await Expect(disclosure.Locator("div").First).ToBeHiddenAsync();
      await disclosure.Locator("summary").FocusAsync();
      await Page.Keyboard.PressAsync("Enter");
      await Expect(disclosure.Locator("div").First).ToBeVisibleAsync();
      await disclosure.Locator("summary").FocusAsync();
      await Page.Keyboard.PressAsync("Space");
      await Expect(disclosure.Locator("div").First).ToBeHiddenAsync();
    }
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var darkMode in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", darkMode);
        Assert.True(await Page.EvaluateAsync<bool>(
            "() => { const main = document.querySelector('main'); return main.scrollWidth <= main.clientWidth && main.getBoundingClientRect().right <= innerWidth; }"),
            $"Career should not overflow at {width}px in {(darkMode ? "dark" : "light")} mode.");
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Career content must not widen the page at {width}px.");
        Assert.True(await startingPoints.EvaluateAsync<bool>(
            "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
            $"Career starting points must fit at {width}px.");
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
        foreach (var disclosure in await Page.Locator("details.guide-disclosure").AllAsync())
        {
          await disclosure.Locator("summary").ClickAsync();
        }
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Expanded career guidance must not widen the page at {width}px.");
        await Expect(Page.Locator("#career-conversation-starter blockquote")).ToBeVisibleAsync();
        foreach (var disclosure in await Page.Locator("details.guide-disclosure").AllAsync())
        {
          await disclosure.Locator("summary").ClickAsync();
        }
      }
    }

    foreach (var link in await startingPoints.GetByRole(AriaRole.Link).AllAsync())
    {
      await link.ClickAsync();
      await Expect(Page.Locator(new Uri(Page.Url).Fragment)).ToBeInViewportAsync();
    }
    foreach (var link in await Page.Locator("nav[aria-label='Career guide topics'] a").AllAsync())
    {
      await link.ClickAsync();
      var target = new Uri(Page.Url).Fragment;
      await Expect(Page.Locator(target)).ToBeInViewportAsync();
    }
    await Page.GetByRole(AriaRole.Link, new() { Name = "Back to career topics" }).First.ClickAsync();
    Assert.Equal("/career/", new Uri(Page.Url).AbsolutePath);
    Assert.Equal("#career-topics", new Uri(Page.Url).Fragment);
    var topicsBox = await Page.Locator("#career-topics").BoundingBoxAsync();
    var headerBox = await Page.Locator("#header").BoundingBoxAsync();
    Assert.NotNull(topicsBox);
    Assert.NotNull(headerBox);
    Assert.True(topicsBox.Y >= headerBox.Y + headerBox.Height,
        "Career topics should not be hidden beneath the sticky header.");
    await Page.GetByRole(AriaRole.Link, new() { Name = "Explore the .NET interview preparation guide" }).ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("C# and .NET Interview Questions and Answers");
  }
}
