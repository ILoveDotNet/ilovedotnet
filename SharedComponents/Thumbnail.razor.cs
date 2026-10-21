using Microsoft.AspNetCore.Components;

namespace SharedComponents;

public class ThumbnailBase : ComponentBase
{
  [Parameter, EditorRequired] public string Title { get; set; } = "Article title goes here and can span some line";
  [Parameter, EditorRequired] public string ContentUrl { get; set; } = default!;
  [Parameter, EditorRequired] public string PosterUrl { get; set; } = "https://via.placeholder.com/285x160.webp";
  [Parameter, EditorRequired] public string AltText { get; set; } = "banner";
  [Parameter, EditorRequired] public string IconUrl { get; set; } = "https://via.placeholder.com/48x48";
  [Parameter, EditorRequired] public string IconAlt { get; set; } = "stack logo";
  [Parameter, EditorRequired] public DateTime CreatedOn { get; set; } = DateTime.Now;
  [Parameter, EditorRequired] public string Author { get; set; } = default!;
  [Parameter, EditorRequired] public string Channel { get; set; } = default!;
  [Parameter] public bool Sidebar { get; set; }
  [Parameter] public int HeadingLevel { get; set; } = 2;

  protected override void OnParametersSet()
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(Channel);
    if (HeadingLevel is not (2 or 3))
    {
      throw new ArgumentOutOfRangeException(nameof(HeadingLevel), HeadingLevel, "Thumbnail headings must be level 2 or 3.");
    }
  }
}
