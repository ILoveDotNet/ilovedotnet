using EndToEndTests.Utilities;
using Microsoft.Playwright;
using System.Reflection;

namespace EndToEndTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class WorkflowTests : BaseTest
{
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