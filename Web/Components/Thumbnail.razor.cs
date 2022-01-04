using Microsoft.AspNetCore.Components;

namespace Web.Components;

public class ThumbnailBase : ComponentBase
{
    [Parameter] public string Title { get; set; } = "Article title goes here and can span some line";
    [Parameter] public string Url { get; set; } = "https://via.placeholder.com/285x160.webp";
    [Parameter] public string AltText { get; set; } = "banner";
    [Parameter] public string Icon { get; set; } = "https://via.placeholder.com/48x48";
    [Parameter] public string IconAlt { get; set; } = "stack logo";
    [Parameter] public DateOnly CreatedOn { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Parameter] public TimeOnly EstimatedTimeToRead { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
}
