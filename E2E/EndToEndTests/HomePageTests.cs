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
}
