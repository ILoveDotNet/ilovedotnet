using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class PageTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  // RecordVideoDir must be set here — before the BrowserContext is created —
  // not on the already-constructed IBrowserContext instance.
  public override BrowserNewContextOptions ContextOptions() => new()
  {
    RecordVideoDir = "videos/page-tests",
    RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 },
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true, // AppHost serves a self-signed dev cert in CI
  };

  public override async ValueTask DisposeAsync()
  {
    var testName = ToFileName(TestContext.Current.Test!.TestDisplayName);
    await base.DisposeAsync().ConfigureAwait(false);
    if (Page.Video is { } video)
    {
      Directory.CreateDirectory("videos/page-tests");
      await video.SaveAsAsync($"videos/page-tests/{testName}.webm");
      await video.DeleteAsync();
    }
  }

  private static string ToFileName(string displayName) =>
      string.Concat(
          displayName.Split('.').Last()
              .Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

  [Fact]
  public async Task BlogPage_Loads_Successfully()
  {
    await Page.GotoAsync($"{WebBaseUrl.AbsoluteUri}blogs/using-github-copilot-ai-for-navigating-new-codebase");
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Using GitHub Copilot AI for Navigating New Codebase" }))
        .ToBeVisibleAsync();
  }
}
