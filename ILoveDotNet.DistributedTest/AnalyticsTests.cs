using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class AnalyticsTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task AnalyticsPage_HasNoIndexAndResponsiveTimelineAndSupportAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "analytics/");
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Analytics");
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    var robots = await SeoTestHelpers.GetRequiredAttributeAsync(Page, "meta[property='robots']", "content");
    Assert.Equal("noindex,follow", robots, ignoreCase: true);

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
        Assert.True(await timeline.EvaluateAsync<bool>(
            "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
            $"The timeline should fit at {width}px in {(dark ? "dark" : "light")} mode.");
        Assert.True(await support.EvaluateAsync<bool>(
            "element => element.scrollWidth <= element.clientWidth && element.getBoundingClientRect().right <= innerWidth"),
            $"The support callout should fit at {width}px in {(dark ? "dark" : "light")} mode.");
        foreach (var link in new[] { star, share })
        {
          var box = await link.BoundingBoxAsync();
          Assert.NotNull(box);
          Assert.True(box.Height >= 44);
        }
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
      }
      await Expect(panel.Locator("tbody tr td")).ToHaveTextAsync(
          expected.Select(record => record.Impressions.ToString("N0", System.Globalization.CultureInfo.InvariantCulture)).ToArray());
      var maximum = expected.Max(x => x.Impressions);
      var heights = await chart.Locator("li div[style]").EvaluateAllAsync<double[]>(
          "bars => bars.map(bar => bar.getBoundingClientRect().height)");
      Assert.Equal(expected.Length, heights.Length);
      var maximumHeight = heights.Max();
      for (var index = 0; index < expected.Length; index++)
      {
        Assert.InRange(Math.Abs(heights[index] / maximumHeight - (double)expected[index].Impressions / maximum), 0, 0.01);
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
}
