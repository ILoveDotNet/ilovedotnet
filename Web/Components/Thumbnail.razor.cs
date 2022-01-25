using Microsoft.AspNetCore.Components;

namespace Web.Components;

public class ThumbnailBase : ComponentBase
{
    [Parameter] public string Title { get; set; } = "Article title goes here and can span some line";
    [Parameter] public string ContentUrl { get; set; } = default!;
    [Parameter] public string PosterUrl { get; set; } = "https://via.placeholder.com/285x160.webp";
    [Parameter] public string AltText { get; set; } = "banner";
    [Parameter] public string IconUrl { get; set; } = "https://via.placeholder.com/48x48";
    [Parameter] public string IconAlt { get; set; } = "stack logo";
    [Parameter] public DateTime CreatedOn { get; set; } = DateTime.Now;
    [Parameter] public string Author { get; set; } = default!;
}
