namespace SharedModels;

public class AuthorMetaData
{
  public static AuthorMetaData NullAuthor => new()
  {
    Order = 0,
    Name = string.Empty,
    Description = string.Empty,
    ImageUrl = string.Empty,
    Role = string.Empty,
    Introduction = string.Empty,
    IndustryExperienceYears = 0,
    IsMVP = false,
    SocialUrls = [],
    Badges = []
  };

  public required sbyte Order { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string ImageUrl { get; set; }
  public required string Role { get; set; }
  public required string Introduction { get; set; }
  public int IndustryExperienceYears { get; set; }
  public bool IsMVP { get; set; }
  public required List<SocialUrl> SocialUrls { get; set; } = [];
  public List<string> Badges = [];
}
