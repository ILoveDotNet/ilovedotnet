namespace SharedModels;

public class DDDLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public DDDLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
      {
        Order = 1,
        Title = "Specification Pattern - Eliminating Scattered Business Logic in DDD",
        Description = "In this post I will teach you how to eliminate scattered and duplicated business logic using the Specification Pattern from Domain-Driven Design. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "ddd-specification-pattern-eliminating-scattered-business-logic",
        PosterUrl = "image/blogs/ddd/ddd-specification-pattern-eliminating-scattered-business-logic.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-specification-pattern-eliminating-scattered-business-logic.webp",
        ContentUrl = "blogs/ddd-specification-pattern-eliminating-scattered-business-logic",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 12, 28, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 12, 28, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Specification Pattern", "Domain-Driven Design", "DDD", "Business Logic", "Entity Framework Core", "Expression Trees", "LINQ", "Design Patterns"]
      }
    ];
  }
}
