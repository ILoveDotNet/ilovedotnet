using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class SlugService(NavigationManager navigationManager)
{
  public string GetSlug()
  {
    var slug = new Uri(navigationManager.Uri).AbsolutePath
        .Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
    if (slug is null)
    {
      throw new ArgumentException("The URL must contain a slug.", nameof(slug));
    }
    ArgumentException.ThrowIfNullOrWhiteSpace(slug, nameof(slug));
    return slug;
  }
}
