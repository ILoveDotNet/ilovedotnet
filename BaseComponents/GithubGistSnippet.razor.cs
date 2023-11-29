using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BaseComponents;

public class GithubGistSnippetBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;

    protected string Id = Guid.NewGuid().ToString();

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter, EditorRequired] public string Title { get; set; } = default!;
    [Parameter, EditorRequired] public string UserId { get; set; } = default!;
    [Parameter, EditorRequired] public string FileName { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Components/githubgist.js");

            await module.InvokeVoidAsync("printSnippetFrom", Id, UserId, FileName);

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
