using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class NotFoundTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

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
}
