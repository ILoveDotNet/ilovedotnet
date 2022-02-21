namespace SharedModels;

public class ContentMetaData
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string PosterUrl { get; set; } = default!;
    public string ThumbnailUrl { get; set; } = default!;
    public string ContentUrl { get; set; } = default!;
    public string IconUrl { get; set; } = default!;
    public string Type { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}