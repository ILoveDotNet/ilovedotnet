using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class FragmentNavigationBase : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            NavigationManager.NavigateTo(NavigationManager.Uri);
        }
    }
}