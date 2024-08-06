using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class BasePage : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private SlugService SlugService { get; set; } = default!;

    protected string Slug { get; set; } = string.Empty;

    override protected void OnInitialized()
    {
        Slug = SlugService.GetSlug();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            NavigationManager.NavigateTo(NavigationManager.Uri);
        }
    }
}