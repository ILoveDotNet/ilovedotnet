namespace SharedModels;

public class TestingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(2);

  public TestingLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Unit Testing Anti-Pattern: Exposing Private State",
          Description = "In this post I will teach you how to avoid common unit testing anti pattern - exposing private state. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "unit-testing-anti-pattern-exposing-private-state",
          PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-exposing-private-state.webp",
          ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-exposing-private-state.webp",
          ContentUrl = "blogs/unit-testing-anti-pattern-exposing-private-state",
          IconUrl = "image/icons/testing.webp",
          Channel = "Testing",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 3, 16, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 3, 16, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Anti Pattern", "Unit Testing" ]
        },
      new ContentMetaData
        {
          Order = 2,
          Title = "Unit Testing Anti-Pattern: Testing Private Methods",
          Description = "In this post I will teach you how to avoid common unit testing anti pattern - testing private methods. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "unit-testing-anti-pattern-testing-private-methods",
          PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-testing-private-methods.webp",
          ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-testing-private-methods.webp",
          ContentUrl = "blogs/unit-testing-anti-pattern-testing-private-methods",
          IconUrl = "image/icons/testing.webp",
          Channel = "Testing",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 3, 24, 23, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 3, 24, 23, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Anti Pattern", "Unit Testing" ]
        }
    ];
  }
}
