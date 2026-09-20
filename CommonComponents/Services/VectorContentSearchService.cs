using Microsoft.JSInterop;
using SharedModels;

namespace CommonComponents.Services;

public sealed class VectorContentSearchService(
  IJSRuntime javaScriptRuntime,
  TableOfContents tableOfContents) : IContentSearchService, IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> _moduleTask = new(() =>
    javaScriptRuntime.InvokeAsync<IJSObjectReference>("import", "./js/vector-search.js").AsTask());
  private readonly IReadOnlyDictionary<string, ContentMetaData> _contentsBySlug = tableOfContents.AllContents
    .ToDictionary(content => content.Slug, StringComparer.OrdinalIgnoreCase);

  public async Task WarmUpAsync(CancellationToken cancellationToken = default)
  {
    var module = await _moduleTask.Value;
    await module.InvokeVoidAsync("warmUp", cancellationToken);
  }

  public async Task<IReadOnlyList<ContentMetaData>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(searchText))
    {
      return [];
    }

    var module = await _moduleTask.Value;
    var slugs = await module.InvokeAsync<string[]>("search", cancellationToken, searchText, 10);
    return
    [
      .. slugs
        .Select(slug => _contentsBySlug.GetValueOrDefault(slug))
        .Where(content => content is not null && content.ModifiedOn.Date <= DateTime.Today)
        .Cast<ContentMetaData>()
    ];
  }

  public async ValueTask DisposeAsync()
  {
    if (!_moduleTask.IsValueCreated)
    {
      return;
    }

    try
    {
      var module = await _moduleTask.Value;
      await module.DisposeAsync();
    }
    catch (JSDisconnectedException exception)
    {
      System.Diagnostics.Debug.WriteLine(exception.Message);
    }
  }
}
