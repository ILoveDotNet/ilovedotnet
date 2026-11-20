using CommonComponents.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using SharedModels;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using SearchComponent = CommonComponents.Shared.Search;

namespace UITests.Components.Search;

[ExcludeFromCodeCoverage]
public class SearchTests
{
  [Fact]
  public async Task PendingAndEmptySearch_AnnounceStateAndOfferLearningPathsAsync()
  {
    var pending = new TaskCompletionSource<IReadOnlyList<ContentMetaData>>(TaskCreationOptions.RunContinuationsAsynchronously);
    var service = new SearchService { Handler = (_, _) => pending.Task };
    await using var ctx = CreateContext(service);
    var cut = ctx.Render<SearchComponent>();
    await cut.Find("#search").TriggerEventAsync("onfocusin", new FocusEventArgs());

    var search = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "unmatched topic" });
    cut.WaitForAssertion(() =>
    {
      Assert.Equal("true", cut.Find("input").GetAttribute("aria-expanded"));
      Assert.Contains("Searching", cut.Find("[role='status']").TextContent);
      Assert.Equal("true", cut.Find("[role='listbox']").GetAttribute("aria-busy"));
    });
    pending.SetResult([]);
    await search;

    Assert.Contains("No matching tutorials", cut.Find("[role='status']").TextContent);
    Assert.Equal("false", cut.Find("[role='listbox']").GetAttribute("aria-busy"));
    Assert.Equal(cut.Find("[role='listbox']").Id, cut.Find("input").GetAttribute("aria-controls"));
    Assert.Equal("/learningpath/", cut.Find("#dropdown-menu a").GetAttribute("href"));
  }

  [Fact]
  public async Task FailedSearch_RetryPreservesQueryAndSupportsKeyboardSelectionAsync()
  {
    var article = new TableOfContents().Contents.First();
    var attempts = 0;
    var service = new SearchService
    {
      Handler = (_, _) => ++attempts == 1
          ? Task.FromException<IReadOnlyList<ContentMetaData>>(new HttpRequestException("offline"))
          : Task.FromResult<IReadOnlyList<ContentMetaData>>([article])
    };
    await using var ctx = CreateContext(service);
    var cut = ctx.Render<SearchComponent>();
    await cut.Find("#search").TriggerEventAsync("onfocusin", new FocusEventArgs());
    const string query = "C# \u7f13\u5b58 \ud83d\ude80";
    await cut.Find("input").InputAsync(new ChangeEventArgs { Value = query });

    Assert.Equal(query, cut.Find("input").GetAttribute("value"));
    Assert.Contains("Check your connection", cut.Find("[role='status']").TextContent);
    await cut.Find("#dropdown-menu button").ClickAsync();
    Assert.Equal(2, attempts);
    Assert.Equal(query, cut.Find("input").GetAttribute("value"));
    Assert.Equal(article.Title, cut.Find("[role='option']").TextContent.Trim());
    await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowDown" });
    Assert.Equal(cut.Find("[role='option']").Id, cut.Find("input").GetAttribute("aria-activedescendant"));
    Assert.Equal("true", cut.Find("[role='option']").GetAttribute("aria-selected"));
    await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });
    Assert.EndsWith(article.ContentUrl, ctx.Services.GetRequiredService<NavigationManager>().Uri);
    Assert.Equal("false", cut.Find("input").GetAttribute("aria-expanded"));
  }

  [Fact]
  public async Task SupersededSearch_CannotRestoreStaleResultsOrSelectionAsync()
  {
    var articles = new TableOfContents().Contents.Take(2).ToArray();
    var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
    var pending = new TaskCompletionSource<IReadOnlyList<ContentMetaData>>(TaskCreationOptions.RunContinuationsAsynchronously);
    var service = new SearchService
    {
      Handler = (query, _) =>
      {
        if (query != "old") return Task.FromResult<IReadOnlyList<ContentMetaData>>([articles[1]]);
        started.SetResult();
        return pending.Task;
      }
    };
    await using var ctx = CreateContext(service);
    var cut = ctx.Render<SearchComponent>();
    await cut.Find("#search").TriggerEventAsync("onfocusin", new FocusEventArgs());
    var oldSearch = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "old" });
    await started.Task.WaitAsync(TimeSpan.FromSeconds(5), Xunit.TestContext.Current.CancellationToken);
    await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "new" });
    await oldSearch;
    pending.SetResult([articles[0]]);

    Assert.Equal(articles[1].Title, cut.Find("[role='option']").TextContent.Trim());
    Assert.Null(cut.Find("input").GetAttribute("aria-activedescendant"));
    await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "ArrowDown" });
    Assert.Equal("true", cut.Find("[role='option']").GetAttribute("aria-selected"));
    await cut.Find("input").InputAsync(new ChangeEventArgs { Value = " " });
    Assert.Empty(cut.FindAll("[role='option']"));
    Assert.Null(cut.Find("input").GetAttribute("aria-activedescendant"));
    Assert.Empty(cut.Find("[role='status']").TextContent);
  }

  [Fact]
  public async Task EscapeDuringPendingSearch_DoesNotReopenSuggestionsAsync()
  {
    var article = new TableOfContents().Contents.First();
    var pending = new TaskCompletionSource<IReadOnlyList<ContentMetaData>>(TaskCreationOptions.RunContinuationsAsynchronously);
    var service = new SearchService { Handler = (_, _) => pending.Task };
    await using var ctx = CreateContext(service);
    var cut = ctx.Render<SearchComponent>();
    await cut.Find("#search").TriggerEventAsync("onfocusin", new FocusEventArgs());
    var search = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "dotnet" });
    await cut.Find("input").KeyDownAsync(new KeyboardEventArgs { Key = "Escape" });
    pending.SetResult([article]);
    await search;

    Assert.Equal("dotnet", cut.Find("input").GetAttribute("value"));
    Assert.Equal("false", cut.Find("input").GetAttribute("aria-expanded"));
    Assert.Empty(cut.Find("[role='status']").TextContent);
    await cut.Find("#search").TriggerEventAsync("onfocusin", new FocusEventArgs());
    Assert.Equal("true", cut.Find("input").GetAttribute("aria-expanded"));
    Assert.Contains("1 suggestion.", cut.Find("[role='status']").TextContent);
  }

  private static BunitContext CreateContext(IContentSearchService service)
  {
    var ctx = new BunitContext();
    ctx.Services.AddSingleton(service);
    ctx.Services.AddLogging();
    ctx.Services.AddHotKeys2();
    ctx.JSInterop.Mode = JSRuntimeMode.Loose;
    ctx.JSInterop.SetupModule(_ => true).Mode = JSRuntimeMode.Loose;
    return ctx;
  }

  private sealed class SearchService : IContentSearchService
  {
    public required Func<string, CancellationToken, Task<IReadOnlyList<ContentMetaData>>> Handler { get; init; }
    public Task WarmUpAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task<IReadOnlyList<ContentMetaData>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
        => Handler(searchText, cancellationToken);
  }
}
