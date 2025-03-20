namespace SharedModels;

public class TestingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(5);

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
        CreatedOn = new DateTime(2025, 3, 23, 23, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 3, 23, 23, 30, 0, DateTimeKind.Utc),
        Keywords = [ "Anti Pattern", "Unit Testing" ]
      },
      new ContentMetaData
      {
        Order = 3,
        Title = "Unit Testing Anti-Pattern: Leaking Domain Knowledge to Tests",
        Description = "In this post I will teach you how to avoid common unit testing anti pattern - leaking domain knowledge to tests. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "unit-testing-anti-pattern-leaking-domain-knowledge-to-tests",
        PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-leaking-domain-knowledge-to-tests.webp",
        ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-leaking-domain-knowledge-to-tests.webp",
        ContentUrl = "blogs/unit-testing-anti-pattern-leaking-domain-knowledge-to-tests",
        IconUrl = "image/icons/testing.webp",
        Channel = "Testing",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 4, 6, 23, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 4, 6, 23, 30, 0, DateTimeKind.Utc),
        Keywords = [ "Anti Pattern", "Unit Testing" ]
      },
      new ContentMetaData
      {
        Order = 4,
        Title = "Unit Testing Anti-Pattern: Code Pollution",
        Description = "In this post I will teach you how to avoid common unit testing anti pattern - code polltion which is adding logic to disable / enable logic only for test. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "unit-testing-anti-pattern-leaking-code-pollution",
        PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-leaking-code-pollution.webp",
        ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-leaking-code-pollution.webp",
        ContentUrl = "blogs/unit-testing-anti-pattern-leaking-code-pollution",
        IconUrl = "image/icons/testing.webp",
        Channel = "Testing",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 4, 13, 23, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 4, 13, 23, 30, 0, DateTimeKind.Utc),
        Keywords = [ "Anti Pattern", "Unit Testing" ]
      },
      new ContentMetaData
      {
        Order = 5,
        Title = "Unit Testing Anti-Pattern: Mocking Concrete Classes",
        Description = "In this post I will teach you how to avoid common unit testing anti pattern - mocking concrete classes. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "unit-testing-anti-pattern-mocking-concrete-classes",
        PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-mocking-concrete-classes.webp",
        ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-mocking-concrete-classes.webp",
        ContentUrl = "blogs/unit-testing-anti-pattern-mocking-concrete-classes",
        IconUrl = "image/icons/testing.webp",
        Channel = "Testing",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 4, 20, 23, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 4, 20, 23, 30, 0, DateTimeKind.Utc),
        Keywords = [ "Anti Pattern", "Unit Testing" ]
      },
      new ContentMetaData
      {
        Order = 5,
        Title = "Unit Testing Anti-Pattern: Working with Time",
        Description = "In this post I will teach you how to avoid common unit testing anti pattern - Working with Time. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "unit-testing-anti-pattern-working-with-time",
        PosterUrl = "image/blogs/testing/unit-testing-anti-pattern-working-with-time.webp",
        ThumbnailUrl = "image/blogs/testing/unit-testing-anti-pattern-working-with-time.webp",
        ContentUrl = "blogs/unit-testing-anti-pattern-working-with-time",
        IconUrl = "image/icons/testing.webp",
        Channel = "Testing",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 4, 27, 23, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 4, 27, 23, 30, 0, DateTimeKind.Utc),
        Keywords = [ "Anti Pattern", "Unit Testing" ]
      }
    ];
  }
}
