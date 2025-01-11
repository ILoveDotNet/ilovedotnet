using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class SlugService(NavigationManager navigationManager)
{
  public string GetSlug()
  {
    var parts = navigationManager.Uri.ToString().Split('/');
    var slug = parts.Last();
    if (string.IsNullOrWhiteSpace(slug) && parts.Length > 4)
    {
      slug = parts[^2];
    }
    ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
    return slug;
  }
}
