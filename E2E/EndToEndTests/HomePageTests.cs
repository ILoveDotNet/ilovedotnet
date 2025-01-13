namespace EndToEndTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
  [Test]
  public async Task HomepageHasCorrectContentAsync()
  {
    await Page.GotoAsync("https://localhost:7176/");

    // Expect a title "to contain" a substring.
    await Expect(Page).ToHaveTitleAsync(new Regex("I ❤️ DotNet"));
  }

  [Test]
  public async Task TestGeneratedUsingPlayWrightRecorderAsync()
  {
    await Page.GotoAsync("https://localhost:7176/");
    await Expect(Page.Locator("#brand")).ToContainTextAsync("I ❤️ .NET");
    await Expect(Page.Locator("#main")).ToContainTextAsync("👉🏼 Click here to Join I ❤️ .NET WhatsApp Channel to get 🔔 notified about new articles and other updates.");
  }
}
