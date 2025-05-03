using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BaseComponents;

public class CodeSnippetBase : ComponentBase, IAsyncDisposable
{
  private IJSObjectReference? module;

  protected string Id = Guid.NewGuid().ToString();

  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

  [Parameter, EditorRequired] public string CssClass { get; set; } = default!;
  [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

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
