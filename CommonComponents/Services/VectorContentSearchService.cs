using Microsoft.JSInterop;
using SharedModels;

namespace CommonComponents.Services;

public sealed class VectorContentSearchService(
  IJSRuntime javaScriptRuntime,
  TableOfContents tableOfContents) : IContentSearchService, IAsyncDisposable
{
  private readonly Lock _moduleLock = new();
  private Task<IJSObjectReference>? _moduleTask;
  private bool _disposed;
  private readonly IReadOnlyDictionary<string, ContentMetaData> _contentsBySlug = tableOfContents.AllContents
    .ToDictionary(content => content.Slug, StringComparer.OrdinalIgnoreCase);

  public async Task WarmUpAsync(CancellationToken cancellationToken = default)
  {
    var module = await GetModuleAsync();
    await module.InvokeVoidAsync("warmUp", cancellationToken);
  }

  public async Task<IReadOnlyList<ContentMetaData>> SearchAsync(string searchText, CancellationToken cancellationToken = default)
  {
    if (string.IsNullOrWhiteSpace(searchText))
    {
      return [];
    }

    var module = await GetModuleAsync();
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
    Task<IJSObjectReference>? moduleTask;
    lock (_moduleLock)
    {
      if (_disposed)
      {
        return;
      }

      _disposed = true;
      moduleTask = _moduleTask;
      _moduleTask = null;
    }

    if (moduleTask is null) return;

    try
    {
      var module = await moduleTask;
      await module.DisposeAsync();
    }
    catch (JSDisconnectedException exception)
    {
      System.Diagnostics.Debug.WriteLine(exception.Message);
    }
    catch (Exception exception)
    {
      System.Diagnostics.Debug.WriteLine(exception.Message);
    }
  }

  private async Task<IJSObjectReference> GetModuleAsync()
  {
    Task<IJSObjectReference> moduleTask;
    lock (_moduleLock)
    {
      ObjectDisposedException.ThrowIf(_disposed, this);
      moduleTask = _moduleTask ??= javaScriptRuntime
        .InvokeAsync<IJSObjectReference>("import", "./js/vector-search.js")
        .AsTask();
    }

    try
    {
      var module = await moduleTask;
      lock (_moduleLock)
      {
        ObjectDisposedException.ThrowIf(_disposed, this);
      }

      return module;
    }
    catch
    {
      lock (_moduleLock)
      {
        if (!_disposed && ReferenceEquals(_moduleTask, moduleTask))
        {
          _moduleTask = null;
        }
      }

      throw;
    }
  }
}
