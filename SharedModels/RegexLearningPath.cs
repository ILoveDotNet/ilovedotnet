namespace SharedModels;

public class RegexLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public RegexLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
        {
          Order = 1,
          Title = "Using Regex to migrate from Fluent Assertions to XUnit Assertions",
          Description = "In this post I will teach you how to use regular expression to migrate from Fluent Assertions to XUnit Assertions. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "using-regex-to-migrate-from-fluent-assertions-to-xunit-assertions",
          PosterUrl = "image/blogs/regex/using-regex-to-migrate-from-fluent-assertions-to-xunit-assertions.webp",
          ThumbnailUrl = "image/blogs/regex/using-regex-to-migrate-from-fluent-assertions-to-xunit-assertions.webp",
          ContentUrl = "blogs/using-regex-to-migrate-from-fluent-assertions-to-xunit-assertions",
          IconUrl = "image/icons/regex.webp",
          Channel = "Regex",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 2, 9, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 2, 9, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["FluentAssertion", "XUnit", "Assertion"]
        }
    ];
  }
}
