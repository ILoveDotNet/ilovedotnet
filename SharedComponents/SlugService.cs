using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class SlugService(NavigationManager navigationManager)
{
    public string GetSlug()
    {
        var slug = navigationManager.Uri.ToString().Split('/').Last();
        ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
        return slug;
    }
}