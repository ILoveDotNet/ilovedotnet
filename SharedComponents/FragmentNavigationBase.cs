using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace SharedComponents;

public class FragmentNavigationBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += TryFragmentNavigation!;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/scrollto.js");

            await NavigationManager.NavigateToFragmentAsync(module);
        }
    }

    private async void TryFragmentNavigation(object sender, LocationChangedEventArgs args)
    {
        await NavigationManager.NavigateToFragmentAsync(module);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }

        NavigationManager.LocationChanged -= TryFragmentNavigation!;
    }
}