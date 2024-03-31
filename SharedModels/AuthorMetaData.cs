namespace SharedModels;

public class AuthorMetaData
{
    public static AuthorMetaData NullAuthor => new()
    {
        Order = 0,
        Name = string.Empty,
        Description = string.Empty,
        ImageUrl = string.Empty,
        IsMVP = false,
        SocialUrls = []
    };

    public required sbyte Order { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public bool IsMVP { get; set; }
    public required List<SocialUrl> SocialUrls { get; set; } = [];
}
