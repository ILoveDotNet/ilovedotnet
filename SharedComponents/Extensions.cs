using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SharedComponents;

public static class Extensions
{
    public static ValueTask NavigateToFragmentAsync(this NavigationManager navigationManager, IJSObjectReference? module)
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

        if (uri.Fragment.Length == 0)
        {
            return default;
        }

        return module?.InvokeVoidAsync("scrollToFragment", uri.Fragment[1..]) ?? default;
    }
}
