using EndToEndTests.Utilities;
using Microsoft.Playwright;
using System.Reflection;

namespace EndToEndTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class WorkflowTests : BaseTest
{
  [SetUp]
  public async Task Setup()
  {
    await Context.Tracing.StartAsync(new()
    {
      Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
      Screenshots = true,
      Snapshots = true,
      Sources = true
    });
  }

  [TearDown]
  public async Task TearDown()
  {
    // This will produce e.g.:
    // bin/Debug/net8.0/playwright-traces/LoggedInCheckOutCancellation.zip
    await Context.Tracing.StopAsync(new()
    {
      Path = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.Name}.zip"
        )
    });
  }

  [Test]
  public async Task CompleteLoginWorkFlowAsync()
  {
    await Page.GotoAsync($"{BaseUrl}/blogs/securing-blazor-wasm-with-oauth-and-oidc-using-identity-server");
    await Page.Locator("#blazordemologin").ClickAsync();
    await Page.GetByPlaceholder("Username").FillAsync("alice");
    await Page.GetByPlaceholder("Username").PressAsync("Tab");
    await Page.GetByPlaceholder("Password").FillAsync("alice");
    await Page.GetByPlaceholder("Password").PressAsync("Enter");
    await Expect(Page.Locator("#main")).ToContainTextAsync("Welcome, Alice Smith");
    await Page.Locator("#blazordemologout").ClickAsync();
    await Page.GetByRole(AriaRole.Link, new() { Name = "here" }).ClickAsync();
    await Expect(Page).ToHaveTitleAsync(new Regex("Securing Blazor WASM with OAuth and OIDC using Identity Server - I ❤️ DotNet"));
    await Expect(Page.Locator("#blazordemologin")).ToContainTextAsync("Login");
  }
}
