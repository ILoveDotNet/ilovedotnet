namespace SharedModels;

public class ContentMetaData
{
  public required sbyte Order { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required string Author { get; set; }
  public required string Slug { get; set; }
  public required string PosterUrl { get; set; }
  public required string ThumbnailUrl { get; set; }
  public required string ContentUrl { get; set; }
  public required string IconUrl { get; set; }
  public required string Channel { get; set; }
  public required string Type { get; set; }
  public required DateTime CreatedOn { get; set; }
  public required DateTime ModifiedOn { get; set; }
  public required List<string> Keywords { get; set; } = [];
  public string? VideoUrl { get; set; }
}
