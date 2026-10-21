using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.Extensions.Logging;

namespace BaseComponents;

public class CodeSnippetBase : ComponentBase, IAsyncDisposable
{
  protected IJSObjectReference? module;

  protected string Id = Guid.NewGuid().ToString();
  protected string Language => CssClass.Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .FirstOrDefault(value => value.StartsWith("language-", StringComparison.Ordinal))?["language-".Length..] switch
  {
    "csharp" or "cs" => "C#",
    "cshtml" or "razor" => "Razor",
    "javascript" or "js" => "JavaScript",
    "json" => "JSON",
    "html" => "HTML",
    "xml" => "XML",
    "css" => "CSS",
    "sql" => "SQL",
    { } value => value,
    null => "Code"
  };
  protected string CopyStatus = "";
  protected bool CopyFailed;

  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private ILogger<CodeSnippetBase> Logger { get; set; } = default!;

  [Parameter, EditorRequired] public string CssClass { get; set; } = default!;
  [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

  protected async Task CopyAsync()
  {
    if (module is null)
    {
      return;
    }

    try
    {
      await module.InvokeVoidAsync("copyCode", Id);
      CopyFailed = false;
      CopyStatus = "Copied to clipboard.";
    }
    catch (JSException exception)
    {
      Logger.LogWarning(exception, "Unable to copy code snippet {SnippetId}", Id);
      CopyFailed = true;
      CopyStatus = "Copy failed. Select the code and copy it manually.";
    }
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./highlight.js");

      await module.InvokeVoidAsync("highlightCode", Id);

      StateHasChanged();
    }
  }

  async ValueTask IAsyncDisposable.DisposeAsync()
  {
    if (module is not null)
    {
      await module.DisposeAsync();
    }
  }
}
