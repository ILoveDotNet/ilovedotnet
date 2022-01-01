using Microsoft.AspNetCore.Components;
using Web.Models;

namespace Web.Shared;

public class AppRootBase : ComponentBase, IDisposable
{
    [Inject] internal AppState AppState { get; set; } = default!;
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override void OnInitialized()
    {
        AppState.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        AppState.OnChange -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
